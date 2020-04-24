public class EntitySQL {
    
    public void EntitySQLExample () {

        using (EntityConnection conn = new EntityConnection ("name=MyEntities")) {
            conn.Open ();

            var queryString = "SELECT VALUE p " +
                "FROM MyEntities.People AS p " +
                "WHERE p.FirstName='Robert'";

            EntityCommand cmd = conn.CreateCommand ();
            cmd.CommandText = queryString;

            using (EntityDataReader rdr =
                cmd.ExecuteReader (CommandBehavior.SequentialAccess |
                    CommandBehavior.CloseConnection)) {
                while (rdr.Read ()) {
                    string firstname = rdr.GetString (1);
                    string lastname = rdr.GetString (2);
                    Console.WriteLine ("{0} {1}", firstname, lastname);
                }
            }
        }
    }
}