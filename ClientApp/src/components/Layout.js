import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu/NavMenu';

const Layout = (props) => {
  const displayName = Layout.name;

    return (
      <div>
        <NavMenu />
        <Container>
          {props.children}
        </Container>
      </div>
    );
};

export default Layout;
