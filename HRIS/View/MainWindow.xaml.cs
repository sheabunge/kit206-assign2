﻿using HRIS.Control;
using System;
using System.Windows;
using System.Windows.Controls;
using HRIS.Teaching;

namespace HRIS.View {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		public MainWindow() {
			InitializeComponent();
			StaffTabContent.StaffDetailsPanel.LoadUnitTimetable += LoadUnitTimetable;
			UnitTabContent.UnitDetailsPanel.LoadStaffDetails += LoadStaffDetails;
		}

		public void LoadUnitTimetable(object sender, EventArgs e) {
			this.UnitsTab.IsSelected = true;
			UnitTabContent.SelectUnit(sender, e);
		}

		public void LoadStaffDetails(object sender, EventArgs e) {
			this.StaffTab.IsSelected = true;
			StaffTabContent.SelectStaffMember(sender, e);
		}
	}
}
