import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { NavLink, NavItem } from "reactstrap";

const AuthenticationLink = () => {
  const { logout, loginWithRedirect, isAuthenticated } = useAuth0()

  return (
    <NavItem>
      {!isAuthenticated && (
        <NavLink id="a" href="#" onClick={() => loginWithRedirect()}>
          Login
        </NavLink>
      )}
      {isAuthenticated && (
        <NavLink
          id="a"
          href="#"
          onClick={() => logout({ returnTo: window.location.origin })}
        >
          Logout
        </NavLink>
      )}
    </NavItem>
  );
};

export default AuthenticationLink;
