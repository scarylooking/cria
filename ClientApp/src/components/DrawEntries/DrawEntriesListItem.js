import React from "react";

function DrawEntriesListItem(props) {

  return (
    <>    
      <p>{props.ticket.key}: {props.ticket.name} - {props.ticket.prize}</p>
    </>
  );
}

export default DrawEntriesListItem;