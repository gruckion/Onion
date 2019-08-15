# Introduction
The purpose of this project is to test out .Net Core with the N-Tier architectural design. Whilst using .Net core and specifically .Net Standard for maximum (sensible) code reuse between the Xamarin.Forms clients (Windows, iOS, and Android) and the ASP.Net Core web application.
The project is setup using DAL and BAL layers. The Unit of work design pattern along with Repositroies and services are used.

Currently the project is the MVC deisng pattern within the ASP.Net Core web application. This is currently being reworked to use Scott Alans feature based approach (outlined in the NDC conference)
https://www.youtube.com/watch?v=szILg-hyFUQ 

In addition to this the MVVM design pattern is being used in the Xamarin client projects with the Prism MVVM framework. These views are built in Xaml while the website views are constructed using Razor.

In the goal here is to improve the code reuse by abstracting out the viewmodels so that they can be used both with Xamarin and ASP.Net Core. Considerations are being made first on how this approach will work with an ASP.Net Core react application. 

An ASP.Net Core Web API has been created that uses JWT and ASP.Net identity auth to provide the register/login/restricted resource access functionality found in ASP.Net Framework projects.
This API is consumed via the Xamarin clients and work is on going to template out the views in XAML.

