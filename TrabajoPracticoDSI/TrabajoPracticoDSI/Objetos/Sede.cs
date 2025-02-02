﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoDSI.Backend;

namespace TrabajoPracticoDSI.Objetos
{
    public class Sede
    {
        public int cantidadMaxVisitantes { get; set; }
        public int cantidadMaxPorGuia{ get; set; }
        public string nombre { get; set; }
        public int id { get; set; }
        public int montoBase { get; set; }

        Conexion_DB _DB = new Conexion_DB();

        DataTable tarifas = new DataTable();
        DataTable exposiciones = new DataTable();
        DataTable reservas = new DataTable();

        public void getSede(int id)
        {
            string sql = $"SELECT * FROM Sede WHERE id = {id}";
            DataTable sede = _DB.EjecutarSelect(sql);

            this.id = id;
            this.cantidadMaxPorGuia = int.Parse(sede.Rows[0]["cantidadMaxPorGuia"].ToString());
            this.cantidadMaxVisitantes = int.Parse(sede.Rows[0]["cantidadMaxVisitantes"].ToString());
            this.montoBase = int.Parse(sede.Rows[0]["montoBase"].ToString());
            this.nombre = sede.Rows[0]["nombre"].ToString();
        }

        public List<Tarifa> obtenerTarifa()
        {
            string sql = $"SELECT * FROM tarifa WHERE idSede = {this.id}";
            tarifas = _DB.EjecutarSelect(sql);

            List<Tarifa> tarifasSede = new List<Tarifa>();

            for (int i = 0; i < tarifas.Rows.Count; i++)
            {
                Tarifa tarifa = new Tarifa();

                tarifa.fechaFinVigencia = tarifas.Rows[i]["fechaFinVigencia"].ToString();
                tarifa.fechaInicioVigencia = tarifas.Rows[i]["fechaInicioVigencia"].ToString();
                tarifa.montoAdicionalGuia = int.Parse(tarifas.Rows[i]["montoAdicionalGuia"].ToString());
                tarifa.idSede = int.Parse(tarifas.Rows[i]["idSede"].ToString());

                if (tarifa.esVigente())
                {
                    int idTipoVisita = int.Parse(tarifas.Rows[i]["idTipoVisita"].ToString());
                    int idTipoEntrada = int.Parse(tarifas.Rows[i]["idTipoEntrada"].ToString());

                    tarifa.getTarifaVigente(idTipoVisita, idTipoEntrada);

                    tarifa.calcularMonto(montoBase);

                    tarifasSede.Add(tarifa);
                }
            }

            return tarifasSede;
        }

        public int obtenerDuracionAExposicionesVigentes()
        {
            int duracionTotal = 0;
            string sql = $"SELECT * FROM Exposicion WHERE idSede = {this.id}";
            exposiciones = _DB.EjecutarSelect(sql);

            for (int i = 0; i < exposiciones.Rows.Count; i++)
            {
                Exposicion exposicion = new Exposicion();
                exposicion.fechaInicio = exposiciones.Rows[i]["fechaInicio"].ToString();
                exposicion.id = int.Parse(exposiciones.Rows[i]["id"].ToString());

                if (exposicion.esVigente())
                {
                    duracionTotal += exposicion.calcularDuracionObrasExpuestas();
                }
            }
            return duracionTotal;

        }

        internal int obtenerCantidadReservasYEntradas()
        {
            string sql1 = $"SELECT * FROM Reservas WHERE idSede = {this.id}";
            reservas = _DB.EjecutarSelect(sql1);

            for (int i = 0; i < reservas.Rows.Count; i++)
            {
                
            }
            int asd = 0;
            return asd;
        }
    }
}
