import React, { useState } from "react";
import { NavLink as RRNavLink, useNavigate } from "react-router-dom";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
} from "reactstrap";
import { logout } from "../modules/authManager";

export default function Header({ isLoggedIn }) {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);

  const navigate = useNavigate()

  return (
    <div>
      <Navbar color="light" light expand="md">
        <NavbarBrand tag={RRNavLink} to="/">
          <img alt="logo" src="./images/If-I-Fitz-logo.png" style={{height: 75, width: 75}}/>
        </NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            {isLoggedIn && (
              <>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/">
                    Home
                  </NavLink>
                </NavItem>
                <NavItem>
                  <a aria-current="page" className="nav-link" style={{cursor: "pointer"}} onClick={logout}>Logout</a>
                </NavItem>
              </>
            )}
            {!isLoggedIn && (
              <>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/login">
                    Login
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/register">
                    Register
                  </NavLink>
                </NavItem>
              </>
            )}
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
}