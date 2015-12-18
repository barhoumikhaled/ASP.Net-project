using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models.Entities
{
    public class CommandeClient
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public string ClientId { get; set; }
        public virtual ApplicationUser Client { get; set; }
        public int DeliveryAddressId { get; set; }
        public virtual Address DeliveryAddress { get; set; }
        public virtual ICollection<EntryCommandeClient> EntryCommandeClient { get; set; }
        //Permet d'arrêter de suivre la commande dans l'historique de commande d'un client (demandé par le client)
        //Ainsi il peut éliminer (visuellement) les commandes
        public bool isTrack { get; set; }

        //Par défaut, lorsqu'une commande est créé, elle est suvi par l'utilisateur
        public CommandeClient() {
            isTrack = true;
        }
    }
}