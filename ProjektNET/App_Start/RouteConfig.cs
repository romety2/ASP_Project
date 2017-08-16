using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjektNET
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

             routes.MapRoute(
                "DodajP",
                "Pogrzeby/Dodaj",
                new { controller = "Pogrzeby", action = "Create", id = UrlParameter.Optional }
            );
                        routes.MapRoute(
                "DodajT",
                "Trupy/Dodaj",
                new { controller = "Trupy", action = "Create", id = UrlParameter.Optional }
            );
                        routes.MapRoute(
                "DodajG",
                "Grabarze/Dodaj",
                new { controller = "Grabarze", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "UsunP",
                "Pogrzeby/Usun/{id}",
                new { controller = "Pogrzeby", action = "Delete", id = UrlParameter.Optional }
            );
                        routes.MapRoute(
                "TrupyUsunT",
                "Trupy/Usun/{id}",
                new { controller = "Trupy", action = "Delete", id = UrlParameter.Optional }
            );
                        routes.MapRoute(
                "UsunG",
                "Grabarze/Usun/{id}",
                new { controller = "Grabarze", action = "Delete", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                "PodgladP",
                "Pogrzeby/Podglad/{id}",
                new { controller = "Pogrzeby", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "PodgladT",
                "Trupy/Podglad/{id}",
                new { controller = "Trupy", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "PodgladG",
                "Grabarze/Podglad/{id}",
                new { controller = "Grabarze", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EdytujP",
                "Pogrzeby/Edytuj/{id}",
                new { controller = "Pogrzeby", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EdytujT",
                "Trupy/Edytuj/{id}",
                new { controller = "Trupy", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EdytujG",
                "Grabarze/Edytuj/{id}",
                new { controller = "Grabarze", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "SzukajP",
                "Pogrzeby/Szukaj",
                new { controller = "Pogrzeby", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "SzukajT",
                "Trupy/Szukaj",
                new { controller = "Trupy", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "SzukajG",
                "Grabarze/Szukaj",
                new { controller = "Grabarze", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RolaS",
                "Role/Sprawdz",
                new { controller = "Role", action = "Check", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RolaZ",
                "Role/Zmien",
                new { controller = "Role", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Logowanie",
                "Logowanie",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Rejestracja",
                "Rejestracja",
                new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
