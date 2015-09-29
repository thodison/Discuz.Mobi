﻿using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Discuz.Api;
using Discuz.Api.Entities;
using Discuz.Api.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.ViewModels {
    public class ForumIndexViewModel : Screen {

        public ObservableCollection<ListViewGroup<ForumDetailViewModel>> Datas {
            get;
            set;
        }

        private INavigationService NS {
            get;
            set;
        }

        public ForumIndexViewModel(INavigationService ns) {
            this.LoadData();
            this.NS = ns;
        }

        private async void LoadData() {
            var method = new ForumIndex();
            var catalogs = await ApiClient.GetInstance().Execute(method);

            var groups = catalogs.Select(c => new ListViewGroup<ForumDetailViewModel>(c.SubFourms.Select(s => new ForumDetailViewModel(s, this.NS))) {
                Title = c.Name
            });

            this.Datas = new ObservableCollection<ListViewGroup<ForumDetailViewModel>>(groups);
            this.NotifyOfPropertyChange(() => this.Datas);
        }
    }
}