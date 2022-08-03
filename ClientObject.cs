using MyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LR_SERVER
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        TcpClient client;
        ServerObject server;
        MyLib.Message m, m1, m2;
        MyLib.ComplexMessage cm = new ComplexMessage();
        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
            Stream = client.GetStream();
        }
        public void Process()
        {
            while (true)
            {
                if (Stream.CanRead)
                {
                    byte[] myReadBuffer = new byte[6297630];
                    do
                    {
                        Stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                    }
                    while (Stream.DataAvailable);
                    Participant participant;
                    Staff staff;
                    Speaker speaker;
                    Tutor tutor;
                    User user;
                    MyLib.ComplexMessage complexMessage = new ComplexMessage();
                    MyLib.Message message = new MyLib.Message();
                    message.Data = myReadBuffer;
                    complexMessage = (ComplexMessage)
                    SerializeAndDeserialize.Deserialize(message);
                    if (complexMessage.NumberStatus == 0)
                    {
                        try
                        {
                            participant = (Participant)SerializeAndDeserialize.Deserialize(complexMessage.First);
                        }
                        catch
                        {
                            participant = null;
                        }
                        try
                        {
                            staff = (Staff)SerializeAndDeserialize.Deserialize(complexMessage.First);
                        }
                        catch
                        {
                            staff = null;
                        }
                        try
                        {
                            speaker = (Speaker)SerializeAndDeserialize.Deserialize(complexMessage.First);
                        }
                        catch
                        {
                            speaker = null;
                        }
                        try
                        {
                            tutor = (Tutor)SerializeAndDeserialize.Deserialize(complexMessage.First);
                        }
                        catch
                        {
                            tutor = null;
                        }

                        user = (User)SerializeAndDeserialize.Deserialize(complexMessage.Second);
                        using (LRContainer2 db = new LRContainer2())
                        {
                            db.UserSet.Add(user);
                            if (user.Role == "Участник")
                            {
                                db.ParticipantSet.Add(participant);
                            }
                            else if (user.Role == "Спикер")
                            {
                                db.SpeakerSet.Add(speaker);
                            }
                            else if (user.Role == "Персонал")
                            {
                                db.StaffSet.Add(staff);
                            }
                            else if (user.Role == "Тьютор")
                            {
                                db.TutorSet.Add(tutor);
                            }
                            db.SaveChanges();
                        }
                    }
                    else if (complexMessage.NumberStatus == 1)
                    {
                        using (LRContainer2 db = new LRContainer2())
                        {
                            byte[] responseData;
                            for (int i = 0; i < db.UserSet.ToList().Count; i++)
                            {
                                if (db.UserSet.ToList()[i].Login == Convert.ToString(SerializeAndDeserialize.Deserialize(complexMessage.First)) && db.UserSet.ToList()[i].Password ==
                                Convert.ToString(SerializeAndDeserialize.Deserialize(complexMessage.Second)))
                                {
                                    User user1 = db.UserSet.ToList()[i];
                                    User user2 = new User() { Login = user1.Login, Password = user1.Password, Role = user1.Role, Email = user1.Email, Image = user1.Image };
                                    m1 = SerializeAndDeserialize.Serialize(user2);
                                    if (db.UserSet.ToList()[i].Role == "Участник")
                                    {
                                        Participant participant1 = db.UserSet.ToList()[i].Participant;
                                        Participant participant2 = new Participant()
                                        {
                                            Login = participant1.Login,
                                            Name = participant1.Name,
                                            Surname = participant1.Surname,
                                            Patronymic = participant1.Patronymic,
                                            Phone = participant1.Phone,
                                            Birtday = participant1.Birtday,
                                            Mail = participant1.Mail,
                                            City = participant1.City,
                                            Direction = participant1.Direction,
                                            Transfer = participant1.Transfer,
                                            Documents = participant1.Documents
                                        };
                                        m2 = SerializeAndDeserialize.Serialize(participant2);
                                    }
                                    else if (db.UserSet.ToList()[i].Role == "Спикер")
                                    {
                                        Speaker speaker1 = db.UserSet.ToList()[i].Speaker;
                                        Speaker speaker2 = new Speaker()
                                        {
                                            Login = speaker1.Login,
                                            Name = speaker1.Name,
                                            Surname  = speaker1.Surname,
                                            Patronymic = speaker1.Patronymic,
                                            Phone = speaker1.Phone,
                                            Mail = speaker1.Mail,
                                            Birthday = speaker1.Birthday,
                                            Direction = speaker1.Direction,
                                            Salary = speaker1.Salary,
                                            Transfer = speaker1.Transfer
                                        };
                                        m2 = SerializeAndDeserialize.Serialize(speaker2);
                                    }
                                    else if (db.UserSet.ToList()[i].Role == "Тьютор")
                                    {
                                        Tutor tutor1 = db.UserSet.ToList()[i].Tutor;
                                        Tutor tutor2 = new Tutor()
                                        { 
                                            Login = tutor1.Login,
                                            Name = tutor1.Name,
                                            Surname = tutor1.Surname,
                                            Patronymic = tutor1.Patronymic,
                                            Phone = tutor1.Phone,
                                            Birthday = tutor1.Birthday,
                                            Direction = tutor1.Direction,
                                            City = tutor1.City,
                                            Childrens = tutor1.Childrens,
                                            Documents = tutor1.Documents
                                        };
                                        m2 = SerializeAndDeserialize.Serialize(tutor2);
                                    }
                                    else if (db.UserSet.ToList()[i].Role == "Персонал")
                                    {
                                        Staff staff1 = db.UserSet.ToList()[i].Staff;
                                        Staff staff2 = new Staff()
                                        {
                                            Login = staff1.Login,
                                            Name = staff1.Name,
                                            Surname = staff1.Surname,
                                            Patronymic = staff1.Patronymic,
                                            Phone = staff1.Phone,
                                            Birthday = staff1.Birthday,
                                            Documents = staff1.Documents,
                                            Place = staff1.Place,
                                            Position = staff1.Position,
                                            R_site = staff1.R_site
                                        };
                                        m2 = SerializeAndDeserialize.Serialize(staff2);
                                    }
                                    else if (db.UserSet.ToList()[i].Role == "Администратор")
                                    {
                                        User user3 = db.UserSet.ToList()[i];
                                        User user4 = new User()
                                        {
                                            Login = user3.Login,
                                            Password = user3.Password,
                                            Email = user3.Email,
                                            Image = user3.Image,
                                            Role = user3.Role
                                        };
                                        m2 = SerializeAndDeserialize.Serialize(user3);
                                    }
                                    cm.First = m1;
                                    cm.Second = m2;
                                    cm.NumberStatus = 2;
                                    m = SerializeAndDeserialize.Serialize(cm);
                                    responseData = m.Data;
                                    Stream.Write(responseData, 0, responseData.Length);
                                    goto label;
                                }
                            }
                            cm.NumberStatus = 3;
                            m = SerializeAndDeserialize.Serialize(cm);

                            responseData = m.Data;
                            Stream.Write(responseData, 0, responseData.Length);
                        label:
                            responseData = null;
                        }
                    }
                }
            }
        }
    }
}
