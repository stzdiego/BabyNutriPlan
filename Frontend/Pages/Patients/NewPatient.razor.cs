using BabyNutriPlan.Shared.Entities;
using BabyNutriPlan.Shared.Enums;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using Radzen.Blazor.Rendering;

namespace Frontend.Pages.Patients
{
    public partial class NewPatient
    {
        private Patient? _patient;
        private List<AttendantPatient> _attendantPatients;
        private IEnumerable<EnumIdentificationType> _identificationsTypes;
        private IEnumerable<EnumGender> _genders;
        private RadzenDataGrid<AttendantPatient>? _attendantsGrid;
        private AttendantPatient? orderToInsert;
        private AttendantPatient? orderToUpdate;
        private List<Attendant>? _attendants;

        public NewPatient()
        {
            _identificationsTypes = GetIdentificationsTypes();
            _genders = GetGenders();
            _attendantPatients = new List<AttendantPatient>();
        }

        private IEnumerable<EnumIdentificationType> GetIdentificationsTypes()
        {
            return Enum.GetValues(typeof(EnumIdentificationType)).Cast<EnumIdentificationType>();
        }

        private IEnumerable<EnumGender> GetGenders()
        {
            return Enum.GetValues(typeof(EnumGender)).Cast<EnumGender>();
        }

        void OnSubmit(Patient model)
        {
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
        }

        void Reset()
        {
            orderToInsert = null;
            orderToUpdate = null;
        }

        async Task EditRow(AttendantPatient model)
        {
            orderToUpdate = model;

            if(_attendantsGrid != null)
                await _attendantsGrid.EditRow(model);
        }

        void OnUpdateRow(AttendantPatient model)
        {
            Reset();

            //dbContext.Update(order);
            //dbContext.SaveChanges();
        }

        async Task SaveRow(AttendantPatient model)
        {
            if(_attendantsGrid != null)
                await _attendantsGrid.UpdateRow(model);
        }

        void CancelEdit(AttendantPatient model)
        {
            Reset();

            if(_attendantsGrid != null)
                _attendantsGrid.CancelEditRow(model);

            /*
            var orderEntry = dbContext.Entry(order);
            if (orderEntry.State == EntityState.Modified)
            {
                orderEntry.CurrentValues.SetValues(orderEntry.OriginalValues);
                orderEntry.State = EntityState.Unchanged;
            }
            */
        }

        async Task DeleteRow(AttendantPatient model)
        {
            Reset();
            /*
            if (_attendants.Contains(model))
            {
                //dbContext.Remove<Order>(model);
                //dbContext.SaveChanges();

                await _attendantsGrid.Reload();
            }
            else
            {
                _attendantsGrid.CancelEditRow(model);
                await _attendantsGrid.Reload();
            }
            */
        }

        async Task InsertRow()
        {
            //orderToInsert = new AttendantPatient();
            if(_attendantsGrid != null && orderToInsert != null)
                await _attendantsGrid.InsertRow(orderToInsert);
        }

        void OnCreateRow(AttendantPatient order)
        {
            //dbContext.Add(order);
            //dbContext.SaveChanges();

            orderToInsert = null;
        }
    }
}