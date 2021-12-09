import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Faculty from './components/Faculty';
import Pulpit from './components/Pulpit';
import SignIn from './components/SignIn';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/Faculty' component={Faculty} />
        <Route exact path='/Pulpit' component={Pulpit} />
        <Route exact path='/SignIn' component={SignIn} />
    </Layout>
);
