using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityBox
{
    public class ChatMessage
    {
        /* *
         * message structure
         * $KrN||command||message type||<sender>||<receiver list , seperated>||message details||KrN$
         * */
        //string rawMessage = string.Empty;

        MessageType messageTyp = MessageType.Messages;

        public MessageType MessageTyp
        {
            get { return messageTyp; }
            set { messageTyp = value; }
        }

        Command messageCommand = Command.Broadcast;

        public Command MessageCommand
        {
            get { return messageCommand; }
            set { messageCommand = value; }
        }

        string sender = "NA";

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        string receivers = "NA";

        public string Receivers
        {
            get { return receivers; }
            set { receivers = value; }
        }

        string messageDetail = "NA";

        public string MessageDetail
        {
            get { return messageDetail; }
            set { messageDetail = value; }
        }

        public ChatMessage()
        {

        }

        public ChatMessage(string msg)
        {
            if (!msg.Contains(Constants.startToken) || !msg.Contains(Constants.endToken))
            {
                throw new InvalidMessageException("Must contain both start and end token");
            }
            string[] splittedMessage = msg.Split(new string[] { "||" }, StringSplitOptions.None);
            if (splittedMessage.Length != 7)
            {
                throw new InvalidMessageException("Invalid format");
            }
            messageCommand = (Command)(int.Parse(splittedMessage[1]));
            messageTyp = (MessageType)(int.Parse(splittedMessage[2]));
            sender = splittedMessage[3];
            receivers = splittedMessage[4];
            messageDetail = splittedMessage[5];
        }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append(Constants.startToken);
            message.Append(Constants.seperator);
            message.Append((int)messageCommand);
            message.Append(Constants.seperator);
            message.Append((int)messageTyp);
            message.Append(Constants.seperator);
            message.Append(sender);
            message.Append(Constants.seperator);
            message.Append(receivers);
            message.Append(Constants.seperator);
            message.Append(messageDetail);
            message.Append(Constants.seperator);
            message.Append(Constants.endToken);

            return message.ToString();
        }
    }
}
