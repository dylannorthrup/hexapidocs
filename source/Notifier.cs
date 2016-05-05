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
using Game.Shared.Tournaments;
using Game.Shared.Network;

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

                public virtual bool ContainsData()
                {
                    return true;
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
                public ulong                       Id    = 0;
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

                    Id = card.CardId.InstanceId;
                }

                public NotifierCardDefinition(card_instance_bits card)
                {
                    if (TemplateManager.Instance.Cards.ContainsKey(card.TemplateID))
                        Guid = card.TemplateID;

                    Id = card.Id;
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

            public class NotifierTournamentPlayer
            {
                public int    Wins;
                public int    Losses;
                public int    Points;
                public string Name;

                public NotifierTournamentPlayer(TournamentPlayerInfo info)
                {
                    Name   = info.Name;
                    Wins   = info.Wins;
                    Losses = info.Losses;
                    Points = info.Points;
                }

                public NotifierTournamentPlayer(string info)
                {
                    Name = info;
                }
            }

            public class NotifierTournamentGame
            {
                public ulong                 ID              = 0;
                public string                PlayerOne       = string.Empty;
                public string                PlayerTwo       = string.Empty;
                public string                GameOneWinner   = string.Empty;
                public string                GameTwoWinner   = string.Empty;
                public string                GameThreeWinner = string.Empty;
                public ETournamentGameStatus Status          = ETournamentGameStatus.Invalid;

                public NotifierTournamentGame(TournamentGameInfo info, List<TournamentPlayerInfo> players)
                {
                    if (players != null)
                        foreach (TournamentPlayerInfo player in players)
                        {
                            if (info.Player1ID   == player.PlayerID) PlayerOne       = player.Name;
                            if (info.Player2ID   == player.PlayerID) PlayerTwo       = player.Name;
                            if (info.Game1Winner == player.PlayerID) GameOneWinner   = player.Name;
                            if (info.Game2Winner == player.PlayerID) GameTwoWinner   = player.Name;
                            if (info.Game3Winner == player.PlayerID) GameThreeWinner = player.Name;

                            ID     = info.ID;
                            Status = info.Status;
                        }
                }
            }

            public class NotifierTournament
            {
                public ulong                          ID            = 0;
                public string                         NextEventTime = string.Empty;
                public ETournamentStyle               Style         = ETournamentStyle.Moderated;
                public ETournamentFormats             Format        = ETournamentFormats.Booster_Draft;
                public List<NotifierTournamentGame>   Games         = new List<NotifierTournamentGame>();
                public List<NotifierTournamentPlayer> Players       = new List<NotifierTournamentPlayer>();

                public NotifierTournament(TournamentInfo info)
                {
                    ID            = info.TournamentID;
                    Style         = info.Style;
                    Format        = info.Format;
                    NextEventTime = info.NextRoundTime.ToString();

                    if (info.Players != null)
                        foreach (TournamentPlayerInfo player in info.Players)
                            Players.Add(new NotifierTournamentPlayer(player));

                    if (info.Games != null)
                        foreach (TournamentGameInfo game in info.Games)
                            Games.Add(new NotifierTournamentGame(game, info.Players));
                }

                public NotifierTournament(TournamentDesc info)
                {
                    ID     = info.TournamentID;
                    Style  = info.Style;
                    Format = info.Format;

                    if (info.Players != null)
                        foreach (string player in info.Players)
                            Players.Add(new NotifierTournamentPlayer(player));
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

            public class Tournament : Notification
            {
                public NotifierTournament TournamentData = null;

                public Tournament(string user, TournamentInfo info) : base(user)
                {
                    try
                    {
                        Message        = GetMessageName();
                        TournamentData = new NotifierTournament(info);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception("API", ex);
                    }
                }

                public Tournament(string user, TournamentDesc info) : base(user)
                {
                    try
                    {
                        Message        = GetMessageName();
                        TournamentData = new NotifierTournament(info);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception("API", ex);
                    }
                }

                public static string GetMessageName()
                {
                    return "Tournament";
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

                    Notifier.SendItemsLock.WaitOne();
                    {
                        try
                        {
                            if (Notifier.CachedSentInventory.Count == 0)
                            {
                                Action = "Overwrite";

                                foreach (KeyValuePair<ResourceId, int> pair in totals)
                                    Complete.Add(new NotifierInventoryDefinition(pair.Key, pair.Value));
                            }
                            else
                            {
                                Action = "Update";

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
                        }
                        catch (Exception ex)
                        {
                            Log.Exception("API", ex);
                        }

                        Notifier.CachedSentInventory = totals;
                    }
                    Notifier.SendItemsLock.ReleaseMutex();
                }

                public override bool ContainsData()
                {
                    if (Action == "Update")
                        if ((ItemsAdded.Count == 0) && (ItemsRemoved.Count == 0))
                            return false;

                    return (base.ContainsData());
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

                    Notifier.SendCardsLock.WaitOne();
                    {
                        try
                        {
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

                            Notifier.CachedSentCards = set;
                        }
                        catch (Exception ex)
                        {
                            Log.Exception("API", ex);
                        }
                    }
                    Notifier.SendCardsLock.ReleaseMutex();
                }

                public override bool ContainsData()
                {
                    if (Action == "Update")
                        if ((CardsAdded.Count == 0) && (CardsRemoved.Count == 0))
                            return false;

                    return (base.ContainsData());
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
                public List<ResourceId>             Equipment;
                public List<NotifierCardDefinition> Deck;
                public List<NotifierCardDefinition> Sideboard;

                public SaveDeck(string user, string name, string champion, List<NotifierCardDefinition> deck, List<NotifierCardDefinition> sideboard, List<ResourceId> equipment)
                    : base(user)
                {
                    Name      = name;
                    Deck      = deck;
                    Message   = GetMessageName();
                    Champion  = champion;
                    Equipment = equipment;
                    Sideboard = sideboard;
                }

                public static string GetMessageName()
                {
                    return ("SaveDeck");
                }
            }

            public class DraftPack : Notification
            {
                public ulong                        TournamentId;
                public List<NotifierCardDefinition> Cards;

                public DraftPack(string user, ulong tournament, List<NotifierCardDefinition> cards)
                    : base(user)
                {
                    Cards        = cards;
                    Message      = GetMessageName();
                    TournamentId = tournament;
                }

                public static string GetMessageName()
                {
                    return ("DraftPack");
                }
            }

            public class DraftCardPicked : Notification
            {
                public ulong                  TournamentId;
                public NotifierCardDefinition Card;

                public DraftCardPicked(string user, ulong tournament, NotifierCardDefinition card)
                    : base(user)
                {
                    Card         = card;
                    Message      = GetMessageName();
                    TournamentId = tournament;
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
                public string                       ChampionName;
                public ETurnPhases                  Phase;
                public Dictionary<ECardShards, int> Thresholds;

                public PlayerUpdated(string user, PlayerRepresentation player, string championName, ETurnPhases phase) : base(user)
                {
                    Id           = player.PlayerId.GetInstanceId();
                    Phase        = phase;
                    Message      = GetMessageName();
                    Resources    = player.TotalResources;
                    Thresholds   = player.GetAllThresholds();
                    ChampionName = championName;
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

        private static List<string> DeprecatedMessages = new List<string>()
        {
            "playerupdated",
            "cardupdated",
        };

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
            string comparer = message.ToLower();

            if (DeprecatedMessages.Contains(comparer))
                return false;

            foreach (KeyValuePair<string, List<string>> pair in m_MessagePairs)
                if ((pair.Value.Contains(comparer)) || (pair.Value.Contains("all")))
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
                        if ((string.IsNullOrEmpty(line) == false) && (line.StartsWith("#") == false))
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
            if (Info.ContainsData())
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

        private static Mutex                                                          m_PendingMutex        = new Mutex();
        private static Thread                                                         m_PendingThread       = null;
        private static AutoResetEvent                                                 m_PendingThreadSignal = new AutoResetEvent(false);
        private static List<KeyValuePair<object, KeyValuePair<string, List<string>>>> m_PendingMessages     = new List<KeyValuePair<object, KeyValuePair<string, List<string>>>>();

        private static void Forward(object Info, KeyValuePair<string, List<string>> Destination)
        {
            if (string.IsNullOrEmpty(Destination.Key) == false)
            {
                m_PendingMutex.WaitOne();
                {
                    if (m_PendingThread == null)
                    {
                        m_PendingThread = new Thread(PendingWorkerThread);
                        m_PendingThread.IsBackground = true;
                        m_PendingThread.Start();
                    }

                    m_PendingMessages.Add(new KeyValuePair<object, KeyValuePair<string, List<string>>>(Info, Destination));
                }
                m_PendingMutex.ReleaseMutex();

                m_PendingThreadSignal.Set();
            }
        }

        private static void PendingWorkerThread()
        {
            WaitHandle[] wait_handles = new WaitHandle[] { m_PendingThreadSignal };

            while (true)
            {
                try
                {
                    WaitHandle.WaitAny(wait_handles);

                    m_PendingMutex.WaitOne();
                        var mesages = new List<KeyValuePair<object, KeyValuePair<string, List<string>>>>(m_PendingMessages);
                        m_PendingMessages.Clear();
                    m_PendingMutex.ReleaseMutex();

                    foreach (KeyValuePair<object, KeyValuePair<string, List<string>>> message in mesages)
                    {
                        try
                        {
                            string seralized_data = JsonConvert.SerializeObject(message.Key);

                            if (String.IsNullOrEmpty(seralized_data) == false)
                            {
                                byte[]                     request_data = Encoding.UTF8.GetBytes(seralized_data);
                                Dictionary<string, string> headers      = new Dictionary<string, string>();

                                headers["Accept"]        = "*/*";
                                headers["UserAgent"]     = "HexClient";
                                headers["Connection"]    = "close";
                                headers["ContentType"]   = "application/json";
                                headers["ContentLength"] = request_data.Length.ToString();

                                HttpReq.CompleteResponse response = HttpReq.GetResponseCustom(message.Value.Key, headers, "POST", request_data, 1000);

                                if ((response != null)
                                &&  (response.HttpResponse != null)
                                &&  (response.HttpResponse.Response != null))
                                    response.HttpResponse.Response.Close();
                            }
                        }
                        catch (Exception)
                        {
                            // We do intentionally ignore this here.  Exceptions can come from the
                            //  other side not properly sending back a response, but I don't want
                            //  those to show up in the log and get reported to us, since we can't
                            //  control them.
                        }
                    }
                }
                catch (ThreadInterruptedException)
                {
                }
                catch (Exception ex)
                {
                    Log.Exception("API", ex);
                }
            }
        }
    }
}
