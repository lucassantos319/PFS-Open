// using System.ComponentModel;
// using API.Controllers;
// using Microsoft.AspNetCore.Http;
// using Newtonsoft.Json;
// using PFS.Domain.Models.Entities.Management;
//
// namespace Tests.API;
//
// public class PainelControllerTester
// {
//     private PainelController _controller;
//     
//     [SetUp]
//     public void Setup()
//     {
//         _controller = new("Host=localhost;Port=5432;UserName=postgres;Password=admin;Database=PFS-MG");
//     }
//
//     [Test]
//     [TestCaseSource(nameof(GetPainels))]
//     public void CreatePainel(PainelCreation painel)
//     {
//         var ok = _controller.Post(painel);
//         
//         
//         Assert.Pass();
//     }
//     
//     public static IEnumerable<PainelCreation> GetPainels()
//     {
//         yield return new PainelCreation() {
//             email = "lucas.sidnei32@gmail.com",
//             painel = JsonConvert.SerializeObject(new Painel()
//             {
//                 name = "teste",    
//             }),
//             role = 1
//         };
//         yield return new PainelCreation() {
//             email = "lucas.sidnei32@gmail.com",
//             painel = JsonConvert.SerializeObject(new Painel()
//             {
//                 name = "teste",    
//             }),
//             role = 1
//         };
//
//     }
// }