import React, { Component, useState } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import AuthenticationLink from '../AuthenticationLink/AuthenticationLink';
import { useAuth0 } from "@auth0/auth0-react";
import './NavMenu.css';

const NavMenu = (props) => {
  const { isAuthenticated } = useAuth0();
  const [navBarIsCollapsed, setNavBarCollapsed] = useState(true);

  const toggleNavbar = () => {
    setNavBarCollapsed(!navBarIsCollapsed)
  }

  return (
    <header>
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
        <Container>
          <NavbarBrand tag={Link} to="/">Cria</NavbarBrand>
          <NavbarToggler onClick={toggleNavbar} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!navBarIsCollapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
              </NavItem>
              {isAuthenticated && <NavItem>
                <NavLink tag={Link} className="text-dark" to="/entries">Entries</NavLink>
              </NavItem>}
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/draw">Enter draw</NavLink>
              </NavItem>
              <AuthenticationLink/>
            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );
}

export default NavMenu;
