using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Human_Resources_Management_System
{
    public class UsersModel
    {
        //For Id
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //For first name
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        //for last name
        [BsonElement("LastName")]
        public string LastName { get; set; }
        //for middle name
        [BsonElement("MiddleName")]
        public string MiddleName { get; set; }
        //for contact no
        [BsonElement("ContactNo")]
        public string ContactNo { get; set; }
        //for email 
        [BsonElement("Email")]
        public string Email { get; set; }
        //for username
        [BsonElement("Username")]
        public string Username { get; set; }
        //for password
        [BsonElement("Password")]
        public string Password { get; set; }
        //for profile image
        [BsonElement("ProfileImage")]
        public byte[] ProfileImage { get; set; }
        //for the approval status
        [BsonElement("ApprovalStatus")]
        public string ApprovalStatus { get; set; }
    }

    public class PeoplesModel
    {
        // for ID
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        // for First Name
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        //for Surname
        [BsonElement("Surname")]
        public string Surname { get; set; }
        //for Middle Name
        [BsonElement("MiddleName")]
        public string MiddleName { get; set; }
        // for Sex
        [BsonElement("Sex")]
        public string Sex { get; set; }
        // for Birthday
        [BsonElement("Birthday")]
        public DateTime Birthday { get; set; }
        // for Age
        [BsonElement("Age")]
        public string Age { get; set; }
        // for Email
        [BsonElement("Email")]
        public string Email { get; set; }
        // for Address
        [BsonElement("Address")]
        public string Address { get; set; }
        // for Contact no.
        [BsonElement("ContactNo")]
        public string ContactNo { get; set; }
        // for Requirements
        [BsonElement("Requirements")]
        public string Requirements { get; set; }
        // for Date hired
        [BsonElement("DateHired")]
        public DateTime DateHired { get; set; }
        // for Shuttle code
        [BsonElement("ShuttleCode")]
        public string ShuttleCode { get; set; }
        // for Emegerncy Contact First Name
        [BsonElement("ContactsFirstName")]
        public string ContactsFirstName { get; set; }
        // for Emergency Contact Surname
        [BsonElement("ContactsSurname")]
        public string ContactsSurname { get; set; }
        // for Emergency Contact Middle Name
        [BsonElement("ContactsMiddleName")]
        public string ContactsMiddleName { get; set; }
        // for Emergency Contact No.
        [BsonElement("ContactsNo")]
        public string ContactsNo { get; set; }
        // for Emergency Contact Sex
        [BsonElement("ContactsSex")]
        public string ContactsSex { get; set; }
        // for Emergencty Contact Address 
        [BsonElement("ContactsAddress")]
        public string ContactsAddress { get; set; }
        // for Profile Picture
        [BsonElement("ProfileImage")]
        public byte[] ProfileImage { get; set; }
        // for applicants signature
        [BsonElement("ApplicantSignature")]
        public byte[] ApplicantSignature { get; set; }
        // for authorize signature
        [BsonElement("AuthorizeSignature")]
        public byte[] AuthorizeSignature { get; set; }
        // for the EmployeeId
        [BsonElement("EmployeeId")]
        public string EmployeeId { get; set; }
        [BsonElement("Pay")]
        public decimal Pay { get; set; }
        [BsonElement("DatePaid")]
        public DateTime DatePaid { get; set; }
        //for the Status
        [BsonElement("Status")]
        public string Status { get; set; }
    }
}
