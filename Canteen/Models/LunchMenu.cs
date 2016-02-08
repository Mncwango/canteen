using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Canteen.Models
{
    public class LunchMenu
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [Required]
        public String DishName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public String ServedAt { get; set; }
        [BsonRepresentation(BsonType.String)]
        
        public HttpPostedFileBase ImagePath { get; set; }

    }

    public class LunchMenuModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [Required]
        public String DishName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public String ServedAt { get; set; }

        public string ImagePath { get; set; }

    }



}