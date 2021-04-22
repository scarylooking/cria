import React, { useEffect, useState, useCallback } from "react";
import DrawEntriesList from "./DrawEntriesList"

function DrawEntries() {
  const [entries, setEntries] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const FetchEntriesHander = useCallback(async () => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await fetch("/api/ticket/all", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization:
            "Bearer SUPER SECRET KEY",
        },
      });

      if (!response.ok) {
        throw new Error(`HTTP ${response.status} - something went wrong!`);
      }

      const data = await response.json();

      const result = data.map((ticket) => {
        return {
          key: ticket.id,
          name: ticket.name,
          prize: ticket.prize,
        };
      });

      setEntries(result);
    } catch (error) {
      setError(error.message);
    }

    setIsLoading(false);
  }, []);

  useEffect(() => {
    FetchEntriesHander();
  }, [FetchEntriesHander]);

  return (
    <>
      {!isLoading && !error && entries.length > 0 && <DrawEntriesList items={entries}/>}
      {!isLoading && !error && entries.length === 0 && <p>No Entries!</p>}
      {isLoading && <p>Loading...</p>}
      {!isLoading && error && <p>{error}</p>}
    </>
  );
}

export default DrawEntries;