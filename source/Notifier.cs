using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using Game.Shared;
using Game.Shared.Mechanics;
using Game.Shared.Domain;
using System.Threading;

namespace Reckoning.Game
{
    public sealed class Notifier
    {
        #region Notification Messages
            public class Notification
            {
                public string User    = string.Empty;
                public string Message = string.Empty;

                public Notification(string user)
                {
                    User = user;
                }
            }

            public class NotifierGemDefinition
            {
                public string     Name = string.Empty;
                public ResourceId Guid = ResourceId.Invalid;

                public NotifierGemDefinition(EGemTypes gem)
                {
                    InventoryGemData gem_data = TemplateManager.Instance.GetGemData(gem);

                    if (gem_data != null)
                    {
                        Name = gem_data.Name;
                        Guid = gem_data.Id;
                    }
                }
            }

            public class NotifierCardDefinition
            {
                public string                      Flags = string.Empty;
                public ResourceId                  Guid  = ResourceId.Invalid;
                public List<NotifierGemDefinition> Gems  = new List<NotifierGemDefinition>();

                public NotifierCardDefinition(ICard card)
                {
                    if (card.Template != null)
                    {
                        Guid = card.Template.m_Id;

                        if (card.IsExtended)
                            Flags = "ExtendedArt";
                    }
                }

                public NotifierCardDefinition(card_instance_bits card)
                {
                    if (TemplateManager.Instance.Cards.ContainsKey(card.TemplateID))
                        Guid = card.TemplateID;
                }

                public void AddGem(EGemTypes gem)
                {
                    Gems.Add(new NotifierGemDefinition(gem));
                }

                public bool Is(NotifierCardDefinition other)
                {
                    if (other != null)
                        if ((Flags == other.Flags) && (Guid == other.Guid))
                            return true;

                    return false;
                }
            }

            public class NotifierInventoryDefinition
            {
                public int        Count = 0;
                public ResourceId Guid  = ResourceId.Invalid;

                public NotifierInventoryDefinition(ResourceId guid, int count)
                {
                    Guid  = guid;
                    Count = count;
                }
            }

            public class NotifierTalentDefinition
            {
                public string     Name = string.Empty;
                public ResourceId Guid = ResourceId.Invalid;

                public NotifierTalentDefinition(ResourceId talent)
                {
                    ChampionTalentData data = TemplateManager.Instance.GetChampionTalentTemplate(talent);

                    if (data != null)
                    {
                        Name = data.Name;
                        Guid = talent;
                    }
                }
            }

            public class NotifierCardSet
            {
                public int        Count = 0;
                public string     Flags = string.Empty;
                public ResourceId Guid  = ResourceId.Invalid;

                public NotifierCardSet(ResourceId guid, string flags, int count)
                {
                    Guid  = guid;
                    Flags = flags;
                    Count = count;
                }
            }

            public class Talents : Notification
            {
                public ERace                          Race;
                public string                         Champion;
                public EChampionClass                 Class;
                public List<NotifierTalentDefinition> Picks;

                public Talents(string user, string champion, ERace race, EChampionClass cl, List<ResourceId> talents) : base(user)
                {
                    Race     = race;
                    Picks    = new List<NotifierTalentDefinition>();
                    Class    = cl;
                    Message  = GetMessageName();
                    Champion = champion;

                    foreach (ResourceId id in talents)
                        Picks.Add(new NotifierTalentDefinition(id));
                }

                public static string GetMessageName()
                {
                    return ("SaveTalents");
                }
            }

            public class Inventory : Notification
            {
                public string                            Action       = string.Empty;
                public List<NotifierInventoryDefinition> Complete     = new List<NotifierInventoryDefinition>();
                public List<NotifierInventoryDefinition> ItemsAdded   = new List<NotifierInventoryDefinition>();
                public List<NotifierInventoryDefinition> ItemsRemoved = new List<NotifierInventoryDefinition>();

                public Inventory(string user, Dictionary<ResourceId, int> totals) : base(user)
                {
                    Message = GetMessageName();

                    if (Notifier.CachedSentInventory.Count == 0)
                    {
                        Action = "Overwrite";

                        foreach (KeyValuePair<ResourceId, int> pair in totals)
                            Complete.Add(new NotifierInventoryDefinition(pair.Key, pair.Value));
                    }
                    else
                    {
                        Action = "Update";

                        Notifier.SendItemsLock.WaitOne();
                        {
                            foreach (ResourceId id in totals.Keys)
                                if (Notifier.CachedSentInventory.ContainsKey(id) == false)
                                    ItemsAdded.Add(new NotifierInventoryDefinition(id, totals[id]));

                            foreach (ResourceId id in Notifier.CachedSentInventory.Keys)
                                if (totals.ContainsKey(id) == false)
                                    ItemsRemoved.Add(new NotifierInventoryDefinition(id, Notifier.CachedSentInventory[id]));

                            foreach (ResourceId id in totals.Keys)
                                if (Notifier.CachedSentInventory.ContainsKey(id))
                                {
                                    int diff = totals[id] - Notifier.CachedSentInventory[id];

                                    if (diff > 0)
                                        ItemsAdded.Add(new NotifierInventoryDefinition(id, diff));
                                    else if (diff < 0)
                                        ItemsRemoved.Add(new NotifierInventoryDefinition(id, -diff));
                                }
                        }
                        Notifier.SendItemsLock.ReleaseMutex();
                    }

                    Notifier.CachedSentInventory = totals;
                }

                public static string GetMessageName()
                {
                    return ("Inventory");
                }
            }

            public class Collection : Notification
            {
                public string                Action       = string.Empty;
                public List<NotifierCardSet> Complete     = new List<NotifierCardSet>();
                public List<NotifierCardSet> CardsAdded   = new List<NotifierCardSet>();
                public List<NotifierCardSet> CardsRemoved = new List<NotifierCardSet>();

                public Collection(string user, Dictionary<CardId, ICard> newCollection) : base(user)
                {
                    Message = GetMessageName();

                    var set   = new Dictionary<ResourceId, Dictionary<string, int>>();
                    var cards = new List<Notifier.NotifierCardDefinition>();

                    foreach (KeyValuePair<CardId, ICard> pair in newCollection)
                        if (pair.Value != null)
                            cards.Add(new Notifier.NotifierCardDefinition(pair.Value));

                    foreach (NotifierCardDefinition card in cards)
                        if (set.ContainsKey(card.Guid) == false)
                        {
                            set[card.Guid] = new Dictionary<string, int>();
                            set[card.Guid][card.Flags] = 1;
                        }
                        else
                        {
                            if (set[card.Guid].ContainsKey(card.Flags))
                                set[card.Guid][card.Flags] = set[card.Guid][card.Flags] + 1;
                            else
                                set[card.Guid][card.Flags] = 1;
                        }

                    if (Notifier.CachedSentCards.Count == 0)
                    {
                        Action = "Overwrite";

                        foreach (ResourceId id in set.Keys)
                            foreach (string flag in set[id].Keys)
                                Complete.Add(new NotifierCardSet(id, flag, set[id][flag]));
                    }
                    else
                    {
                        Action = "Update";

                        Notifier.SendCardsLock.WaitOne();
                        {
                            try
                            {
                                foreach (ResourceId id in set.Keys)
                                    if (Notifier.CachedSentCards.ContainsKey(id))
                                    {
                                        foreach (string str in set[id].Keys)
                                            if (Notifier.CachedSentCards[id].ContainsKey(str) == false)
                                                CardsAdded.Add(new NotifierCardSet(id, str, set[id][str]));
                                    }
                                    else
                                    {
                                        foreach (string str in set[id].Keys)
                                            CardsAdded.Add(new NotifierCardSet(id, str, set[id][str]));
                                    }

                                foreach (ResourceId id in Notifier.CachedSentCards.Keys)
                                    if (set.ContainsKey(id))
                                    {
                                        foreach (string str in Notifier.CachedSentCards[id].Keys)
                                            if (set[id].ContainsKey(str) == false)
                                                CardsRemoved.Add(new NotifierCardSet(id, str, Notifier.CachedSentCards[id][str]));
                                    }
                                    else
                                    {
                                        foreach (string str in Notifier.CachedSentCards[id].Keys)
                                            CardsAdded.Add(new NotifierCardSet(id, str, Notifier.CachedSentCards[id][str]));
                                    }

                                foreach (ResourceId id in set.Keys)
                                    if (Notifier.CachedSentCards.ContainsKey(id))
                                        foreach (string str in set[id].Keys)
                                            if (Notifier.CachedSentCards[id].ContainsKey(str))
                                            {
                                                int delta = set[id][str] - Notifier.CachedSentCards[id][str];

                                                if (delta > 0)
                                                    CardsAdded.Add(new NotifierCardSet(id, str, delta));
                                                else if (delta < 0)
                                                    CardsRemoved.Add(new NotifierCardSet(id, str, -delta));
                                            }
                            }
                            catch (Exception ex)
                            {
                                Log.Exception("API", ex);
                            }
                        }
                        Notifier.SendCardsLock.ReleaseMutex();
                    }

                    Notifier.CachedSentCards = set;
                }

                public static string GetMessageName()
                {
                    return ("Collection");
                }
            }

            public class Login : Notification
            {
                public Login(string user) : base(user)
                {
                    Message = GetMessageName();
                }

                public static string GetMessageName()
                {
                    return ("Login");
                }
            }

            public class Logout : Notification
            {
                public Logout(string user) : base(user)
                {
                    Message = GetMessageName();
                }

                public static string GetMessageName()
                {
                    return ("Logout");
                }
            }

            public class SaveDeck : Notification
            {
                public string                       Name;
                public string                       Champion;
                public List<NotifierCardDefinition> Deck;
                public List<NotifierCardDefinition> Sideboard;

                public SaveDeck(string user, string name, string champion, List<NotifierCardDefinition> deck, List<NotifierCardDefinition> sideboard)
                    : base(user)
                {
                    Name      = name;
                    Deck      = deck;
                    Message   = GetMessageName();
                    Champion  = champion;
                    Sideboard = sideboard;
                }

                public static string GetMessageName()
                {
                    return ("SaveDeck");
                }
            }

            public class DraftPack : Notification
            {
                public List<NotifierCardDefinition> Cards;

                public DraftPack(string user, List<NotifierCardDefinition> cards)
                    : base(user)
                {
                    Cards   = cards;
                    Message = GetMessageName();
                }

                public static string GetMessageName()
                {
                    return ("DraftPack");
                }
            }

            public class DraftCardPicked : Notification
            {
                public NotifierCardDefinition Card;

                public DraftCardPicked(string user, NotifierCardDefinition card)
                    : base(user)
                {
                    Card    = card;
                    Message = GetMessageName();
                }

                public static string GetMessageName()
                {
                    return ("DraftCardPicked");
                }
            }

            public class GameStarted : Notification
            {
                public List<string> Players;

                public GameStarted(string user, List<string> players) : base(user)
                {
                    Players = players;
                    Message = GetMessageName();
                }

                public static string GetMessageName()
                {
                    return ("GameStarted");
                }
            }

            public class GameEnded : Notification
            {
                public List<string> Winners;
                public List<string> Losers;

                public GameEnded(string user, List<string> winners, List<string> losers) : base(user)
                {
                    Losers  = losers;
                    Winners = winners;
                    Message = GetMessageName();
                }

                public static string GetMessageName()
                {
                    return ("GameEnded");
                }
            }

            public class PlayerUpdated : Notification
            {
                public int                          Resources;
                public ulong                        Id;
                public Dictionary<ECardShards, int> Thresholds;

                public PlayerUpdated(string user, PlayerRepresentation player) : base(user)
                {
                    Id         = player.PlayerId.GetInstanceId();
                    Message    = GetMessageName();
                    Resources  = player.TotalResources;
                    Thresholds = player.GetAllThresholds();
                }

                public static string GetMessageName()
                {
                    return ("PlayerUpdated");
                }
            }

            public class CardUpdated : Notification
            {
                public int              Cost;
                public int              Attack;
                public int              Defense;
                public ulong            Controller;
                public string           Name;
                public EGemTypes        Gems;
                public ResourceId       BaseTemplate;
                public ECardStates      State;
                public ECardShards      Shards;
                public List<string>     Abilities;
                public ECardAttributes  Attributes;
                public ECardCollections Collection;

                public CardUpdated(string user, CardRepresentation card) : base(user)
                {
                    try
                    {
                        Cost         = card.Cost;
                        Gems         = card.Gems;
                        Name         = card.Name;
                        State        = card.State;
                        Attack       = card.Attack;
                        Shards       = card.Colors;
                        Defense      = card.Defense;
                        Message      = GetMessageName();
                        Attributes   = card.Attributes;
                        Controller   = card.Controller.GetInstanceId();
                        Collection   = card.Collection;
                        BaseTemplate = card.TemplateId;

                        Abilities = new List<string>();

                        foreach (ResourceId id in card.Abilities)
                            if (TemplateManager.Instance.Abilities.ContainsKey(id))
                                Abilities.Add(TemplateManager.Instance.Abilities[id].m_GameText);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception("Notifier", ex);
                    }
                }

                public static string GetMessageName()
                {
                    return ("CardUpdated");
                }
            }
        #endregion

        public static Mutex                                           SendCardsLock       = new Mutex();
        public static Mutex                                           SendItemsLock       = new Mutex();
        public static string                                          ConfigFileName      = "api.ini";
        public static Dictionary<ResourceId, int>                     CachedSentInventory = new Dictionary<ResourceId, int>();
        public static Dictionary<ResourceId, Dictionary<string, int>> CachedSentCards     = new Dictionary<ResourceId, Dictionary<string, int>>();

        private static object                    sync           = new Object();
        private static volatile Notifier         m_Instance;
        private Dictionary<string, List<string>> m_MessagePairs = new Dictionary<string, List<string>>();


        private Notifier()
        {
            // CW - useful for debugging.  Should always be remarked in P4
            //m_MessagePairs["http://127.0.0.1:5000"] = new List<string>() { "all" };
        }

        public static Notifier Instance
        {
            get
            {
                if (m_Instance == null)
                    lock (sync)
                        if (m_Instance == null)
                            m_Instance = new Notifier();

                return (m_Instance);
            }
        }

        public bool CaresAbout(string message)
        {
            foreach (KeyValuePair<string, List<string>> pair in m_MessagePairs)
                if ((pair.Value.Contains(message.ToLower())) || (pair.Value.Contains("all")))
                    return true;

            return false;
        }

        public void LoadConfigFromFile(string filename)
        {
            if (File.Exists(filename))
                using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
                {
                    string line = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] entries = line.Split('|');

                        if (entries.Length > 1)
                        {
                            m_MessagePairs[entries[0]] = new List<string>();

                            for (int i = 1; i < entries.Length; ++i)
                                m_MessagePairs[entries[0]].Add(entries[i].ToLower());
                        }
                    }
                }
        }

        public void Send(Notification Info)
        {
            Send(Info.Message, Info);
        }

        private void Send(string MessageType, object Info)
        {
            try
            {
                string comparer = MessageType.ToLower();

                foreach (KeyValuePair<string, List<string>> destination in m_MessagePairs)
                    if ((destination.Value.Contains(comparer)) || (destination.Value.Contains("all")))
                    {
                        Log.Info("API", "Sending message {0} to {1}.", comparer, destination.Key);
                        Forward(Info, destination);
                    }
            }
            catch (Exception ex)
            {
                Log.Exception("Notifier", ex);
            }
        }

        private void Forward(object Info, KeyValuePair<string, List<string>> Destination)
        {
            if (string.IsNullOrEmpty(Destination.Key) == false)
            {
                ThreadPool.QueueUserWorkItem(
                    (url) =>
                    {
                        try
                        {
                            string seralized_data = JsonConvert.SerializeObject(Info);

                            if (String.IsNullOrEmpty(seralized_data) == false)
                            {
                                byte[]         request_data = Encoding.UTF8.GetBytes(seralized_data);
                                string         result       = string.Empty;
                                HttpWebRequest request      = (HttpWebRequest)(HttpWebRequest.Create(Destination.Key));

                                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                                request.Method        = "POST";
                                request.Accept        = "*/*";
                                request.Timeout       = 5000;
                                request.UserAgent     = "HexClient";
                                request.ContentType   = "application/json";
                                request.ContentLength = request_data.Length;

                                using (var data_stream = request.GetRequestStream())
                                {
                                    data_stream.Write(request_data, 0, request_data.Length);
                                    data_stream.Flush();
                                }

                                using (var response = request.GetResponse())
                                    using (var stream = response.GetResponseStream())
                                        using (var reader = new StreamReader(stream))
                                            result = reader.ReadToEnd();
                            }
                        }
                        catch (Exception)
                        {
                            // We do intentionally ignore this here.  Exceptions can come from the
                            //  other side not properly sending back a response, but I don't want
                            //  those to show up in the log and get reported to us, since we can't
                            //  control them.
                        }
                    }, Destination.Key);
            }
        }
    }
}