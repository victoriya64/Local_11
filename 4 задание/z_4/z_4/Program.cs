using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Использован способ XML DOM.");
            // Создаем объект XmlDocument
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:/Users/Виктория/Documents/конструирование/4 задание/doc.xml");

            // Создаем объект XmlWriter
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(@"C:/Users/Виктория/Documents/конструирование/4 задание/output.xml", settings))
            {
                // Открываем элемент <catalog>
                writer.WriteStartElement("catalog");

                // Получаем все элементы <department>
                XmlNodeList departments = doc.GetElementsByTagName("department");
                foreach (XmlNode department in departments)
                {
                    // Создаем элемент <department> и копируем атрибуты
                    writer.WriteStartElement("department");
                    writer.WriteAttributeString("id", department.Attributes["id"].Value);

                    // Получаем элемент <boss> и записываем его содержимое
                    XmlNode bossNode = department.SelectSingleNode("boss");
                    if (bossNode != null)
                    {
                        writer.WriteStartElement("boss");
                        writer.WriteString(bossNode.InnerText);
                        writer.WriteEndElement();
                    }

                    // Получаем все элементы <worker>
                    XmlNodeList workers = department.SelectNodes("workers/worker");
                    if (workers.Count > 0)
                    {
                        // Создаем элемент <workers>
                        writer.WriteStartElement("workers");

                        foreach (XmlNode worker in workers)
                        {
                            // Создаем элемент <worker> и копируем атрибуты
                            writer.WriteStartElement("worker");
                            writer.WriteAttributeString("name", worker.Attributes["name"].Value);

                            // Получаем элемент <specialization> и записываем его содержимое
                            XmlNode specializationNode = worker.SelectSingleNode("specialization");
                            if (specializationNode != null)
                            {
                                writer.WriteStartElement("specialization");
                                writer.WriteString(specializationNode.InnerText);
                                writer.WriteEndElement();
                            }

                            // Закрываем элемент <worker>
                            writer.WriteEndElement();
                        }

                        // Закрываем элемент <workers>
                        writer.WriteEndElement();
                    }

                    // Закрываем элемент <department>
                    writer.WriteEndElement();
                }

                // Закрываем элемент <catalog>
                writer.WriteEndElement();

                // Закрываем объект XmlWriter
                writer.Close();
            }

            Console.WriteLine("В файл output информация записана.");
            Console.ReadKey();
        }
    }