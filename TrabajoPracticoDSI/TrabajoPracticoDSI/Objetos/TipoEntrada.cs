﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoDSI.Backend;

namespace TrabajoPracticoDSI.Objetos
{
    public class TipoEntrada
    {
        Conexion_DB _DB = new Conexion_DB();
        public int id { get; set; }
        public int porcentaje { get; set; }
        public string nombre { get; set; }

        public void getNombreTipoEntrada(int idTipoEntrada)
        {
            string sql = $"SELECT * FROM Tipo_Entrada WHERE id = {idTipoEntrada}";
            DataTable tipoEntrada = _DB.EjecutarSelect(sql);
            this.nombre = tipoEntrada.Rows[0]["nombre"].ToString();
            this.porcentaje =int.Parse(tipoEntrada.Rows[0]["porcentaje"].ToString());
        }
    }
}
