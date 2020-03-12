using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorDemo.ChatApp
{
    public abstract class TeamMember
    {
        private Chatroom chatroom;

        public TeamMember(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        internal void SetChatroom(Chatroom chatroom)
        {
            this.chatroom = chatroom;
        }

        public void Send(string message)
        {
            this.chatroom.Send(this.Name, message);
        }

        public virtual void Receive(string from, string message)
        {
            Console.WriteLine($"from {from}: '{message}'");
        }

        public void SendTo<T>(string message) where T : TeamMember
        {
            this.chatroom.SendTo<T>(this.Name, message);
        }
    }
}
