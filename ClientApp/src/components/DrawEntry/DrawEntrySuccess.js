import React from 'react';
import { Check2Circle } from 'react-bootstrap-icons';

const DrawEntrySuccess = (props) => {

  return (
    <div className="jumbotron">
      <h1 className="display-4"><Check2Circle className="text-success" /> You're In!</h1>
      <p className="lead">Your entry was recorded successfully.</p>
      <hr className="my-4" />
      <p>Your ticket ID is <b>{props.ticketId}</b></p>
    </div>
  );
}

export default DrawEntrySuccess;