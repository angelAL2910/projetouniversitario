﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSVirtualOffice.Models.querydata
{
    public class CustomerdataID
    {
        public enum Illussortorder
        {
            clientname = 1,
            Idno = 2,
            phoneno = 3,
            emailid1 = 4,
            lastupdated = 5,
            none = 6,

        }

        private String clientname;
        private String firstname;
        private String lastname;
        private String middlename;
        private DateTime lastupdated;
        private String emailid1;
        private String phoneno;
        private String appointment;
        private String idno;
        private long customerno;


        public CustomerdataID(String clientname, String firstname, String lastname, String middlename, String lastupdated, String emailid1, String phoneno, String appointment, String idno,long customerno)
        {
            DateTime dt = Convert.ToDateTime(lastupdated);
            this.clientname = clientname;
            this.firstname = firstname;
            this.lastname = lastname;
            this.middlename = middlename;
            this.lastupdated = dt;
            this.phoneno = phoneno;
            this.emailid1 = emailid1;
            this.appointment = appointment;
            this.idno = idno;
            this.customerno = customerno;

        }

        public String Clientname
        {
            get { return this.clientname; }
            set { this.clientname = value; }
        }

        public String Firstname
        {
            get { return this.firstname; }
            set { this.firstname = value; }
        }

        public String Lastname
        {
            get { return this.lastname; }
            set { this.lastname = value; }
        }

        public String Middlename
        {
            get { return this.middlename; }
            set { this.middlename = value; }
        }

        public DateTime Lastupdated
        {
            get { return this.lastupdated; }
            set { this.lastupdated = value; }
        }

        public String Emailid1
        {
            get { return this.emailid1; }
            set { this.emailid1 = value; }
        }

        public String Phoneno
        {
            get { return this.phoneno; }
            set { this.phoneno = value; }
        }

        public String Appointment
        {
            get { return this.appointment; }
            set { this.appointment = value; }
        }

        public String Idno
        {
            get { return this.idno; }
            set { this.idno = value; }
        }
        public long Customerno
        {
            get { return this.customerno; }
            set { this.customerno = value; }
        }






    }
}
