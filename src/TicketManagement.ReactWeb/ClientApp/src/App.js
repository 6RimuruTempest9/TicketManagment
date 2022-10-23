import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';

import './custom.css'
import { Login } from './components/auth/Login';
import { Registration } from './components/auth/Registration';
import { EventsTable } from './components/events/EventsTable';
import { EditEvent } from './components/events/EditEvent';
import { AreasTable } from './components/events/AreasTable';
import { SeatsTable } from './components/events/SeatsTable';
import { Profile } from './components/Profile';
import { AddBalance } from './components/AddBalance';
import { EditProfile } from './components/EditProfile';
import { ChangePassword } from './components/ChangePassword';
import { CreateEvent } from './components/events/CreateEvent';
import { PurchaseHistory } from './components/PurchaseHistory';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={EventsTable} />
        <Route exact path='/events/getall' component={EventsTable} />
        <Route path="/authorization/login" component={Login} />
        <Route path="/authorization/register" component={Registration} />
        <Route path="/events/create" component={CreateEvent} />
        <Route path="/events/update" component={EditEvent} />  
        <Route path="/eventareas/getallbyeventid" component={AreasTable} />
        <Route path="/eventseats/getallbyeventareaid" component={SeatsTable} />
        <Route path="/user/profile" component={Profile} />
        <Route path="/user/addbalance" component={AddBalance} />
        <Route path="/user/changePassword" component={ChangePassword} />
        <Route path="/user/history" component={PurchaseHistory} />
        <Route path="/user/edit" component={EditProfile} />
      </Layout>
    );
  }
}
