import * as React from 'react';
import { Route, Routes } from "react-router-dom";
import Layout from './components/Layout';
import Home from './components/Home';
import Faculty from './components/Faculty';
import Pulpit from './components/Pulpit';
import Profession from './components/Profession';
import Group from './components/Group';
import SignIn from './components/SignIn';
import SignUp from './components/SignUp';
import AddRoleToUser from './components/AddRoleToUser';
import AddGroupToStudent from './components/AddGroupToStudent';
import ControlledRadioButtonsGroup from './components/ControlledRadioButtonsGroup';
import SelectCourse from './components/SelectCourse';

import './custom.css'

export default () => (
    <Layout>
        <Routes>
            <Route path={'/'} element={<Home/>} />
            <Route path={'/Faculty'} element={<Faculty/>} />
            <Route path={'/Pulpit'} element={<Pulpit/>} />
            <Route path={'/Profession'} element={<Profession/>} />
            <Route path={'/Group'} element={<Group/>} />
            <Route path={'/SignIn'} element={<SignIn/>} />
            <Route path={'/SignUp'} element={<SignUp/>} />
            <Route path={'/AddRoleToUser'} element={<AddRoleToUser/>} />
            <Route path={'/AddGroupToStudent'} element={<AddGroupToStudent/>} />
        </Routes>
    </Layout>
);
