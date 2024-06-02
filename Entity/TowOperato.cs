namespace TowFinder.Entity
{
    public class TowOperator
    {
        /*
    CREATE TABLE TowOperators (
    TowOperatorID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    City VARCHAR(100) NOT NULL,
    District VARCHAR(100) NOT NULL,
    ApprovalStatus BOOLEAN DEFAULT FALSE
);
*/
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        
        public bool ApprovalStatus { get; set; }


    }
}
