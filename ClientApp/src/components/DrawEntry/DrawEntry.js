import React, { useState } from 'react';
import DrawEntryForm from "./DrawEntryForm";
import DrawEntrySuccess from "./DrawEntrySuccess";
import DrawEntryFailure from "./DrawEntryFailure";

const DrawEntry2 = () => {
  const [isError, setIsError] = useState(false);
  const [ticketId, setTicketId] = useState(null);

  return (
    <>
      <DrawEntryFailure isVisible={isError}/>
      {!ticketId && <DrawEntryForm errorHandler={setIsError} ticketHandler={setTicketId}  />}
      {ticketId && <DrawEntrySuccess ticketId={ticketId} />}
    </>
  );
}

export default DrawEntry2;