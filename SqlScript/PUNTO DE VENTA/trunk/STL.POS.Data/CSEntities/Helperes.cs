﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace STL.POS.Data.CSEntities
{
    public class Helperes
    {
        public static int QuantityOfMonth(DateTime dt, DateTime df)
        {
            int monthsApart = 12 * (dt.Year - df.Year) + dt.Month - df.Month;
            return Math.Abs(monthsApart);
        }

        public static int QuantityOfDays(DateTime dt, DateTime df)
        {
            int DaysApart = df.Date.Subtract(dt.Date).Days;
            return Math.Abs(DaysApart);
        }

        public static string Encrypt(object Argument)
        {
            byte[] keyArray;
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(Argument.ToString());
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncriptionKey"]));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        public static string Decrypt(object Argument)
        {
            byte[] keyArray;
            byte[] Array_a_Descifrar = Convert.FromBase64String(Argument.ToString());
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["EncriptionKey"]));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
            tdes.Clear();
            string res = UTF8Encoding.UTF8.GetString(resultArray);
            return res;
        }
        public static string Encrypt_Query(string cadena)
        {
            string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
            try
            {

                byte[] keyArray;
                //arreglo de bytes donde guardaremos el texto  
                //que vamos a encriptar  
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(cadena);
                //se utilizan las clases de encriptación  
                //provistas por el Framework  
                //Algoritmo MD5     
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                //se guarda la llave para que se le realice  
                //hashing          
                keyArray = hashmd5.ComputeHash(

                UTF8Encoding.UTF8.GetBytes(key));



                hashmd5.Clear();


                //Algoritmo 3DAS  
                TripleDESCryptoServiceProvider tdes =

                new TripleDESCryptoServiceProvider();


                tdes.Key = keyArray;

                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;


                //se empieza con la transformación de la cadena  
                ICryptoTransform cTransform = tdes.CreateEncryptor();

                //arreglo de bytes donde se guarda la  
                //cadena cifrada  

                byte[] ArrayResultado =

                 cTransform.TransformFinalBlock(Arreglo_a_Cifrar,

                0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena  

                return Convert.ToBase64String(ArrayResultado,

                      0, ArrayResultado.Length);

            }

            catch (Exception)
            {

            }

            return string.Empty;

        }

        public static string Decrypt_Query(string clave)
        {
            string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
            try
            {

                byte[] keyArray;
                //convierte el texto en una secuencia de bytes  

                byte[] Array_a_Descifrar =
              Convert.FromBase64String(clave);

                //se llama a las clases que tienen los algoritmos  

                //de encriptación se le aplica hashing  

                //algoritmo MD5  
                MD5CryptoServiceProvider hashmd5 =
               new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(
                 UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes =
                new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;

                tdes.Padding = PaddingMode.PKCS7;


                ICryptoTransform cTransform =

                 tdes.CreateDecryptor();


                byte[] resultArray =

                cTransform.TransformFinalBlock(Array_a_Descifrar,
                0, Array_a_Descifrar.Length);


                tdes.Clear();

                //se regresa en forma de cadena  
                return UTF8Encoding.UTF8.GetString(resultArray);
            }

            catch (Exception)
            {

            }

            return string.Empty;

        }

        public static string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }

        public static string SerializeToXMLString(object obj)
        {
            string xmlString = string.Empty;
            XmlSerializer s = new XmlSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, new UTF8Encoding());
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = ' ';
            writer.Indentation = 5;

            try
            {
                s.Serialize(writer, obj);
                //xmlString = ASCIIEncoding.UTF8.GetString(ms.ToArray());
                xmlString = UTF8Encoding.UTF8.GetString(ms.ToArray());

            }
            finally
            {
                writer.Close();
                ms.Close();
            }

            return
                xmlString;
        }


        public class GpItemCardTypes
        {
            public List<string> CARDNAME { get; set; }
        }

        public class ItemAcuerdos
        {
            public int Cuota { get; set; }
            public System.DateTime FechaCuota { get; set; }
            public decimal MontoCuota { get; set; }
            public decimal PorcInicial { get; set; }
        }
        public enum GpFrecuencytype { Daily = 1, Weekly = 2, Biweekly = 3, Monthly = 4, Bimonthly = 5, Quarterly = 6, Halfyearly = 7, Yearly = 8, Onetime = 9 };
        public enum RequestType
        {
            Emision = 1,
            Inclusion = 2,
            Exclusion = 3,
            Renovacion = 4
        }
    }
}