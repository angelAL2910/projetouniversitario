﻿using System;

namespace WEB.UnderWriting.Case.UserControls.Summary
{
    public partial class UCUnderwritingComments : WEB.UnderWriting.Common.UC, WEB.UnderWriting.Common.IUC
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void UnderWriting.Common.IUC.Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        void UnderWriting.Common.IUC.save()
        {
            throw new NotImplementedException();
        }

        void UnderWriting.Common.IUC.readOnly(bool x)
        {
            throw new NotImplementedException();
        }

        void UnderWriting.Common.IUC.edit()
        {
            throw new NotImplementedException();
        }

        void UnderWriting.Common.IUC.FillData()
        {
            throw new NotImplementedException();
        }

        public void FillData(String Comment) {

            txtUnderwritingComment.Text = Comment;
        
        }


        public void clearData()
        {
            throw new NotImplementedException();
        }
    }
}