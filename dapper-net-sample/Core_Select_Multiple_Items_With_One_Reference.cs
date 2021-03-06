﻿using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using dapper_net_sample.Entity;
using dapper_net_sample.Utility;

namespace dapper_net_sample
{
    /// <summary>
    /// Select one entity object with children collection 
    /// </summary>
    public class Core_Select_Multiple_Items_With_One_Reference
    {
        public static void Main()
        {
            using (var sqlConnection = new SqlConnection(Constant.DatabaseConnection))
            {
                sqlConnection.Open();

                IEnumerable<Product> products = sqlConnection
                    .Query<Product, Supplier, Product>(
                        @"select Products.*, Suppliers.* 
                          from Products join Suppliers 
                               on Products.SupplierId = Suppliers.Id
                               and suppliers.Id = 2",
                        (a, s) =>
                            {
                                a.Supplier = s;
                                return a;
                            }); // use splitOn, if the id field is not Id or ID

                foreach (Product product in products)
                {
                    ObjectDumper.Write(product.Supplier);
                }
            }
        }
    }
}