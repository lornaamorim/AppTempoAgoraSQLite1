﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppTempoAgoraSQLite.Models
{
    public class Tempo


    {
        string _descricao;

        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        public string? Cidade { get; set; }
        public DateTime DataConsulta { get; set; }
        public double? lon { get; set; }
        public double? lat { get; set; }
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }
        public int? visibility { get; set; }
        public double? speed { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public string? sunrise { get; set; }
        public string? sunset { get; set; }

    }
}