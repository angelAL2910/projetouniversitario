﻿using Entity.UnderWriting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WEB.NewBusiness.Common;

namespace WEB.NewBusiness.NewBusiness.UserControls.VehicleInspectionForm
{
    public partial class TipoCombustible : UC, IUC
    {
        public static bool? Enabled { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void FillData()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            var options = ObjServices.oVehicleManager.GetVehicleReviewItemOption(new Vehicle.Review
            {
                CorpId = ObjServices.Corp_Id,
                LanguageId = ObjServices.Language.ToInt(),
                ReviewGroupId = null
            }).Select(o => new Vehicle.InspectionForm.Option
            {
                ClassDesc = o.ClassDesc,
                ClassId = o.ClassId.Value,
                GroupDesc = o.GroupDesc,
                GroupId = o.GroupId,
                ItemDesc = o.ItemDesc,
                ItemId = o.ItemId != null ? o.ItemId.Value : -1,
                OptionDesc = o.OptionDesc,
                OptionId = o.OptionId != null ? o.OptionId.Value : -1
            }).Where(c => c.GroupId.GetValueOrDefault().Equals(Utility.ReviewGroups.TipoCombustible.ToInt())).ToList();

            if (options.Count > 0)
            {
                var radios = new Dictionary<string, int>() { };
                foreach (var radio in this.Controls.OfType<RadioButton>())
                {
                    radios.Add(radio.ClientID, radio.Attributes["ItemId"].ToInt());

                    int? ItemId = Convert.ToInt32(radio.Attributes["ItemId"]);
                    var option = options.FirstOrDefault(o => o.ItemId == ItemId);

                    radio.InputAttributes.Add("data-review-group-id", Convert.ToString(option.GroupId.GetValueOrDefault()));
                    radio.InputAttributes.Add("data-review-class-id", Convert.ToString(option.ClassId.GetValueOrDefault()));
                    radio.InputAttributes.Add("data-review-item-id", Convert.ToString(option.ItemId.GetValueOrDefault()));
                    radio.InputAttributes.Add("data-review-option-id", Convert.ToString(option.OptionId.GetValueOrDefault()));

                    if (!string.IsNullOrEmpty(ObjServices.FuelTypeDesc) && ObjServices.FuelTypeDesc != "No Definido" && ObjServices.FuelTypeDesc != "N/A")
                    {
                        bool r = Utility.FuelTypeMatch(ObjServices.FuelTypeDesc, option.ItemDesc);
                        if (r)
                        {
                            radio.Checked = true;
                            ObjServices.FuelTypeDesc = null;
                        }
                    }
                }

                foreach (var label in this.Controls.OfType<HtmlGenericControl>())
                {
                    var radio = radios.FirstOrDefault(r => r.Value == label.Attributes["ItemId"].ToInt());
                    label.Attributes.Add("for", radio.Key);

                    int? ItemId = label.Attributes["ItemId"].ToInt();
                    var option = options.FirstOrDefault(o => o.ItemId == ItemId);

                    label.InnerText = HttpUtility.HtmlDecode(option.ItemDesc);
                }
            }
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }
    }
}