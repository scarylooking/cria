import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import DrawEntry from './components/DrawEntry/DrawEntry';
import DrawEntries from './components/DrawEntries/DrawEntries';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/draw' component={DrawEntry} />
        <Route path='/entries' component={DrawEntries} />
      </Layout>
    );
  }
}
