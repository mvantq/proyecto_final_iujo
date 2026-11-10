using System;
using System.Text;

namespace proyecto_final_iujo
{
    public static class LoreAntropologico
    {
        public static string ObtenerTratadoAldea()
        {
            // armamos el parrafo con el stringbuilder para no concatenar tanto
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("«LA CONDICION HUMANA FRENTE AL LIMITE DE LA ALDEA»");
            sb.AppendLine("El aislamiento como constructo social responde a la necesidad antropologica");
            sb.AppendLine("de protegerse de la angustia existencial ante lo desconocido.");
            sb.AppendLine("Los tabúes y las fronteras son las murallas que la comunidad erige");
            sb.AppendLine("para otorgar orden al caos del bosque exterior.");
            return sb.ToString();
        }

        // devuelve la descripcion segun el elemento que le pasemos
        public static string ObtenerMito(string elemento)
        {
            // pasamos a minusculas para que no de error por escribir fuego o FUEGO
            switch (elemento.ToLower())
            {
                // arquetipo de la faccion fuego
                case "fuego":
                    return "Arquetipo de Ares: la pasión bélica y la transgresión violenta del orden.";

                // arquetipo de la faccion agua
                case "agua":
                    return "Arquetipo de Poseidón: la profundidad emocional y la erosión constante del tiempo.";

                // arquetipo de la faccion tierra
                case "tierra":
                    return "Arquetipo de Gea: el fundamento sobre el que se levanta la aldea y su resistencia ontológica.";

                // arquetipo de viento o aire
                case "viento":
                case "aire":
                    return "Arquetipo de Hermes y Zeus: la palabra que trasciende las empalizadas y el aliento vital.";

                // por si no coincide con ningun elemento valido
                default:
                    return "Entidad fronteriza sin catalogar.";
            }
        }
    }
}