using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class StaffRepo : IRepo<Staff>
    {
        public readonly TContext _context;
        public StaffRepo(TContext context)
        {
            _context = context;

        }

        public Task deleteAllAsync(List<Staff> data)
        {
            throw new NotImplementedException();
        }

        public async Task deleteAsync(int ID)
        {
            try
            {
                var staff = await _context.Staffs.FindAsync(ID);
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Staff>> getAllAsync()
        {
            try
            {
                var staff = await _context.Staffs.Include(x => x.Qualifications).ToListAsync();
                return staff;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Staff> getAsync(int ID)
        {
            try
            {
                var staff = await _context.Staffs.Include(x => x.Qualifications).Where(x => x.ID == ID).FirstOrDefaultAsync();
                return staff;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> insertAsync(Staff data)
        {
            var staff = new Staff();
            try
            {
                if (data != null)
                {
                    staff = new Staff
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserModified,
                        FirstName = data.FirstName,
                        Address = data.Address,
                        Email = data.Email,
                        Gender = data.Gender,
                        HEL = data.HEL,
                        DateofBirth = data.DateofBirth,
                        Image = data.Image,
                        LastName = data.LastName,
                        Phone = data.Phone,
                        SecondPhone = data.SecondPhone,
                        DateEmployed = data.DateEmployed,
                        Designation = data.Designation,
                        StaffID = data.StaffID,
                        MaidenName = data.MaidenName,
                        MaritalStatus = data.MaritalStatus,
                        MiddleName = data.MiddleName,
                        NextofKin = data.NextofKin,
                        NextofKinAddress = data.NextofKinAddress,
                        NextofKinPhone = data.NextofKinPhone,
                        ProfilePictureUrl = data.ProfilePictureUrl,
                        RelationshipToNextofKin = data.RelationshipToNextofKin,
                        YearofMarriage = data.YearofMarriage

                    };
                    await _context.Staffs.AddAsync(staff);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return staff.ID;
        }

        public Task<int> insertListAsync(List<Staff> data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> updateAsync(Staff data)
        {
            var staff = await _context.Staffs.FindAsync(data.ID);
            try
            {
                if (staff != null)
                {

                    
                    if (data.SecondPhone != null) staff.SecondPhone = data.SecondPhone;
                    if (data.Image != null) staff.Image = data.Image;
                    if (data.FirstName != null) staff.FirstName = data.FirstName;
                    if (data.Phone != null) staff.Phone = data.Phone;
                    if (data.LastName != null) staff.LastName = data.LastName;
                    staff.DateModified = DateTime.Now;
                    staff.UserModified = data.UserModified;
                    if (data.Gender != null) staff.Gender = data.Gender;
                    if (data.Email != null) staff.Email = data.Email;
                    if (data.Address != null) staff.Address = data.Address;
                    if (data.HEL != null) staff.HEL = data.HEL;
                    if (data.DateEmployed != null) staff.DateEmployed = data.DateEmployed;
                    if (data.Designation != null) staff.Designation = data.Designation;
                    if (data.MaidenName != null) staff.MaidenName = data.MaidenName;
                    if (data.MaritalStatus != null) staff.MaritalStatus = data.MaritalStatus;
                    if (data.MiddleName != null) staff.MiddleName = data.MiddleName;
                    if (data.NextofKin != null) staff.NextofKin = data.NextofKin;
                    if (data.NextofKinAddress != null) staff.NextofKinAddress = data.NextofKinAddress;
                    if (data.NextofKinPhone != null) staff.NextofKinPhone = data.NextofKinPhone;
                    if (data.ProfilePictureUrl != null) staff.ProfilePictureUrl = data.ProfilePictureUrl;
                    if (data.RelationshipToNextofKin != null) staff.RelationshipToNextofKin = data.RelationshipToNextofKin;
                    if (data.YearofMarriage != null) staff.YearofMarriage = data.YearofMarriage;

                    _context.Staffs.Update(staff);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return staff.ID;
        }
    }
}
