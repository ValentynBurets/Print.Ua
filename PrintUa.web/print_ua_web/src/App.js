import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import {
  BrowserRouter as Router,
  Route
} from "react-router-dom";

import AuthProvider from './Components/AuthProvider/AuthProvider';
import AuthorizedRoute from './Components/RoleRoutes/AuthorizedRoute';

import Layout from './Components/Layout';
import StartPage from './Pages/StartPage/StartPage';
import NewOrderPage from './Pages/NewOrderPage/NewOrderPage';
import OrderListPage from './Pages/OrderListPage/OrderListPage';
import UserListPage from './Pages/UserListPage/UserListPage';
import EmployeeOrderPage from './Pages/EmployeeOrderPage/EmployeeOrderPage';
import AdminServicePage from './Pages/AdminServicePage/AdminServicePage';
import EmployeeCurProductPage from './Pages/EmployeeCurProductPage/EmployeeCurProductPage';
import ViewOrderPage from './Pages/OrderListPage/ViewOrderPage/ViewOrderPage';
import AdminNewServicePage from './Pages/AdminNewServicePage/AdminNewServicePage'
import AdminEditServicePage from './Pages/AdminEditServicePage/AdminEditServicePage';
import ProfilePage from './Pages/ProfilePage/ProfilePage';

function App() {

  return (
    <AuthProvider>
      <Router>
        <Layout>
          {/* to add a new page just add a route here */}
          <Route exact path="/" component={StartPage} />
          <AuthorizedRoute exact path="/view_order/:id_order" component={ViewOrderPage} role={"Customer"} />
          <AuthorizedRoute exact path="/profile" component={ProfilePage} role={"All"} />
          <AuthorizedRoute exact path="/order_list" component={OrderListPage} role={"Customer"} />
          <AuthorizedRoute exact path="/new_order" component={NewOrderPage} role={"Customer"} />
          <AuthorizedRoute exact path="/cur_product/:id_product/" component={EmployeeCurProductPage} role={"Employee"} />
          <AuthorizedRoute exact path="/employee_order_page" component={EmployeeOrderPage} role={"Employee"} />
          <AuthorizedRoute exact path="/admin/service" component={AdminServicePage} role={"Admin"} />
          <AuthorizedRoute exact path="/admin/service/:id_service" component={AdminEditServicePage} role={"Admin"} />
          <AuthorizedRoute exact path="/admin/new_service/" component={AdminNewServicePage} role={"Admin"} />
          <AuthorizedRoute exact path="/user_list" component={UserListPage} role={"Admin"} />
        </Layout>
      </Router>
    </AuthProvider>
  );
}

export default App;