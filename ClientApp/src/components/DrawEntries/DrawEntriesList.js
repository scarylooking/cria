import React, { Component } from 'react';
import DrawEntriesListItem from './DrawEntriesListItem';

function DrawEntriesList(props) {
  return (
    <>
      {props.items.map((ticket) => (
        <DrawEntriesListItem ticket={ticket} />
      ))}
      <p>{props.items.length} {props.items.length === 1 ? 'entry' : 'entries'}</p>
    </>
  );
}

export default DrawEntriesList;
