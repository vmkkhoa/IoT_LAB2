using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using DG.Tweening;
using M2MqttUnity;
using Newtonsoft.Json;



public class Status
{
    public string projectid = "L01_HK212";
    public string projectname = "App IoT";
    public string stationid = "1812670";
    public string stationname = "Minh Khoa";
    public string longitude = "";
    public string latitude = "";
    public string temperature = "36";
    public string humidity = "50";
}


namespace M2MqttUnity.Examples
{
    public class Manager : M2MqttUnityClient
{
    public CanvasGroup layer1;
    public CanvasGroup layer2;
    public Text error_display; 
    public InputField broker;
    public InputField username;
    public InputField password;
    public Button connectButton;
    public Button disconnectButton;
    public Button testPublishButton;
    public Button clearButton;
    public Toggle_Switch _toggleLED;
    public Toggle_Switch _togglePump;

        public void Display()
    {
        // _text.text = "Hello World";
    }
    public void ChangeScene()
    {                
     }
    public void BackScene()
    {
            layer1.alpha = 1;
            layer2.alpha = 0;
            layer2.transform.DOLocalMoveX(layer2.transform.localPosition.x + 3000, 0.2f);
     }
    public string Topic;
    public string Machine_Id;
    public string Topic_to_Subcribe = "";
        public string TopicLED = "";
        public string TopicPump = "";
    public string msg_received_from_topic = "";
    public Text text_display;
        public Text _temperature;
        public Text _humidity;
        public Text _stationname;
        public Text _stationid;
        public string projectname;
        public string stationid;
        public string stationname;
        public string temperature;
        public string humidity;
        private List<string> eventMessages = new List<string>();
    private bool updateUI = false;
        public Status _status = new Status { };


        //private void Awake()
        //{
        //    Topic_to_Subcribe = Topic + Machine_Id;
        //}
        public void TestPublish()
    {
            string msg_sent_to_topic = JsonConvert.SerializeObject(_status, Formatting.Indented);
        client.Publish(Topic_to_Subcribe, System.Text.Encoding.UTF8.GetBytes(msg_sent_to_topic), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        Debug.Log("Test message published");
        AddUiMessage("Test message published.");
    }

        //public void SetBrokerAddress(string brokerAddress)
        //{
        //    if (addressInputField && !updateUI)
        //    {
        //        this.brokerAddress = brokerAddress;
        //    }
        //}

        //public void SetBrokerPort(string brokerPort)
        //{
        //    if (portInputField && !updateUI)
        //    {
        //        int.TryParse(brokerPort, out this.brokerPort);
        //    }
        //}

        public override void Connect()
        {
            base.brokerAddress = broker.text;
            base.mqttUserName = username.text;
            base.mqttPassword = password.text;
            base.Connect();    
            
        }

        public void SetEncrypted(bool isEncrypted)
    {
        this.isEncrypted = isEncrypted;
    }
        public void PublishLED()
        {
            if(_toggleLED.switchState == -1)
            {
                string msg_publish_to_led = @"{'device':'LED','status':'ON'}";
                client.Publish(TopicLED, System.Text.Encoding.UTF8.GetBytes(msg_publish_to_led), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                Debug.Log("LED message published");
            }           
            //AddUiMessage("LED message published.");
        }
        public void PublishPump()
        {
            if(_togglePump.switchState == -1)
            {
                string msg_publish_to_pump = @"{'device':'PUMP','status':'OFF'}";
                client.Publish(TopicPump, System.Text.Encoding.UTF8.GetBytes(msg_publish_to_pump), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                Debug.Log("PUMP message published");
            }
            
            //AddUiMessage("LED message published.");
        }


        //public void SetUiMessage(string msg)
        //{
        //    if (consoleInputField != null)
        //    {
        //        consoleInputField.text = msg;
        //        updateUI = true;
        //    }
        //}

        public void AddUiMessage(string msg)
    {
        //if (consoleInputField != null)
        //{
        //    consoleInputField.text += msg + "\n";
        //    updateUI = true;
        //}
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
        //SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
    }

    protected override void OnConnected()
    {
        base.OnConnected();
            //SetUiMessage("Connected to broker on " + brokerAddress + "\n");

            //if (autoTest)
            //{
            TestPublish();
            //}           
            SubscribeTopics();

            //change scene
            layer1.alpha = 0;
            layer2.alpha = 1;
            layer2.transform.DOLocalMoveX(layer2.transform.localPosition.x - 3000, 0.2f);
        }

    protected override void SubscribeTopics()
    {
        if (Topic_to_Subcribe != "")
        {
            client.Subscribe(new string[] { Topic_to_Subcribe }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
    }

    protected override void UnsubscribeTopics()
    {
        client.Unsubscribe(new string[] { Topic_to_Subcribe });
    }

        protected override void OnConnectionFailed(string errorMessage)
        {
            AddUiMessage("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            AddUiMessage("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            AddUiMessage("CONNECTION LOST!");
        }

        private void UpdateUI()
    {
        //if (client == null)
        //{
        //    if (connectButton != null)
        //    {
        //        connectButton.interactable = true;
        //        disconnectButton.interactable = false;
        //        testPublishButton.interactable = false;
        //    }
        //}
        //else
        //{
        //    if (testPublishButton != null)
        //    {
        //        testPublishButton.interactable = client.IsConnected;
        //    }
        //    if (disconnectButton != null)
        //    {
        //        disconnectButton.interactable = client.IsConnected;
        //    }
        //    if (connectButton != null)
        //    {
        //        connectButton.interactable = !client.IsConnected;
        //    }
        //}
        //if (addressInputField != null && connectButton != null)
        //{
        //    addressInputField.interactable = connectButton.interactable;
        //    addressInputField.text = brokerAddress;
        //}
        //if (portInputField != null && connectButton != null)
        //{
        //    portInputField.interactable = connectButton.interactable;
        //    portInputField.text = brokerPort.ToString();
        //}
        //if (encryptedToggle != null && connectButton != null)
        //{
        //    encryptedToggle.interactable = connectButton.interactable;
        //    encryptedToggle.isOn = isEncrypted;
        //}
        //if (clearButton != null && connectButton != null)
        //{
        //    clearButton.interactable = connectButton.interactable;
        //}
        //updateUI = false;
    }

        protected override void Start()
    {
        //SetUiMessage("Ready.");
        Topic_to_Subcribe = Topic + Machine_Id;
        updateUI = true;
        base.Start();
    }

    protected override void DecodeMessage(string topic, byte[] message)
    {
        string msg = System.Text.Encoding.UTF8.GetString(message);
        msg_received_from_topic = msg;
        

        text_display.text = msg;
        _temperature.text = JsonConvert.DeserializeObject<Status>(msg).temperature;
        _humidity.text = JsonConvert.DeserializeObject<Status>(msg).humidity;
        _stationid.text = JsonConvert.DeserializeObject<Status>(msg).stationid;
        _stationname.text = JsonConvert.DeserializeObject<Status>(msg).stationname;

            Debug.Log("Received: " + msg);
        StoreMessage(msg);
        if (topic == Topic_to_Subcribe)
        {
            //if (autoTest)
            //{
            //    autoTest = false;
            //    Disconnect();
            //}
        }
    }

    private void StoreMessage(string eventMsg)
    {
        eventMessages.Add(eventMsg);
    }

    private void ProcessMessage(string msg)
    {
        AddUiMessage("Received: " + msg);
    }

    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()

        if (eventMessages.Count > 0)
        {
            foreach (string msg in eventMessages)
            {
                ProcessMessage(msg);
            }
            eventMessages.Clear();
        }
        if (updateUI)
        {
            UpdateUI();
        }
    }

    private void OnDestroy()
    {
            Disconnect();            
    }

    private void OnValidate()
    {
        //if (autoTest)
        //{
        //    autoConnect = true;
        //}
    }
}

}
