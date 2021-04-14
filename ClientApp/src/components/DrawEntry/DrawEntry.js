import React, { useState } from 'react';
import DrawEntryForm from "./DrawEntryForm";
import ErrorBanner from "../UI/ErrorBanner";

const DrawEntry = () => {
  const [isError, setIsError] = useState(false);

  return (
    <div className="container">

      <div className="row mb-5">
        <div className="col-lg-12 text-center">
          <h1 className="mt-5">Enter Draw</h1>
        </div>
      </div>

      <ErrorBanner isVisible={true} />
      <DrawEntryForm errorHandler={setIsError} />

    </div>
  );
}

export default DrawEntry;