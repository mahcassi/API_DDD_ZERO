using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notify
    {
        public Notify() {
            Notifications = new List<Notify>();
        }

        [NotMapped]
        public string PropertyName { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public List<Notify> Notifications { get; set; }


        public bool ValidatePropertyString(string value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(propertyName))
            {
                Notifications.Add(new Notify
                {
                    Message = "Campo Obrigatório!",
                    PropertyName = propertyName
                });

                return false;
            }
            return true;
        }

    }
}
