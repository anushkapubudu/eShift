using eShift.Business.Interface;
using eShift.Business.Services;
using eShift.Model;
using eShift.Repository;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace eShift.Forms.Admin
{
    public partial class FrmAdminManageContainer : Form
    {
        private readonly IContainerService _containerService;
        private List<Container> allContainers = new List<Container>();
        private Container editingContainer = null;

        public FrmAdminManageContainer()
        {
            InitializeComponent();

            IContainerRepository containerRepo = new ContainerRepository();
            _containerService = new ContainerService(containerRepo);

            BindContainerTypes();
            LoadContainers();

            dtgvContainer.CellClick += dtgvContainer_CellClick;
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void BindContainerTypes()
        {
            cmbContainerType.DataSource = Enum.GetValues(typeof(ContainerType));
        }

        private void LoadContainers()
        {
            allContainers = _containerService.GetAllContainers();
            dtgvContainer.DataSource = allContainers;

            dtgvContainer.Columns["ContainerId"].Visible = false;
            dtgvContainer.Columns["DeletedAt"].Visible = false;

            dtgvContainer.Columns["ContainerType"].HeaderText = "Type";
            dtgvContainer.Columns["CapacityKg"].HeaderText = "Capacity (kg)";

            dtgvContainer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCapacity.Text) || cmbContainerType.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCapacity.Text.Trim(), out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Capacity must be a positive number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var containerType = (ContainerType)cmbContainerType.SelectedItem;

            if (editingContainer != null)
            {
                // UPDATE logic
                editingContainer.ContainerType = containerType.ToString(); 
                editingContainer.CapacityKg = capacity;

                bool updated = _containerService.UpdateContainer(editingContainer);
                MessageBox.Show(updated ? "Updated successfully." : "Update failed.",
                    updated ? "Success" : "Error",
                    MessageBoxButtons.OK, updated ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                editingContainer = null;
                btnSubmit.Text = "Add";
            }
            else
            {
                // CREATE logic
                var container = new Container
                {
                    ContainerType = containerType.ToString(),  
                    CapacityKg = capacity
                };

                try
                {
                    _containerService.AddContainer(container);
                    MessageBox.Show("Container added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add container.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ClearFields();
            LoadContainers();
        }

        private void dtgvContainer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                editingContainer = (Container)dtgvContainer.Rows[e.RowIndex].DataBoundItem;
                txtCapacity.Text = ((int)Math.Floor(editingContainer.CapacityKg)).ToString();
                if (Enum.TryParse<ContainerType>(editingContainer.ContainerType, out var parsedType))
                {
                    cmbContainerType.SelectedItem = parsedType;
                }
                else
                {
                    cmbContainerType.SelectedIndex = -1;
                }

                btnSubmit.Text = "Update";
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            var filtered = allContainers.Where(c =>
                c.ContainerType.ToString().ToLower().Contains(keyword) ||
                c.CapacityKg.ToString().Contains(keyword)
            ).ToList();

            dtgvContainer.DataSource = filtered;
        }

        private void ClearFields()
        {
            txtCapacity.Text = string.Empty;
            cmbContainerType.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (editingContainer == null)
            {
                MessageBox.Show("Select a container to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Delete container of type {editingContainer.ContainerType}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _containerService.DeleteContainer(editingContainer.ContainerId);
                MessageBox.Show("Container deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                editingContainer = null;
                btnSubmit.Text = "Add";
                ClearFields();
                LoadContainers();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
