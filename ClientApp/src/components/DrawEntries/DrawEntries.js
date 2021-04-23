import React, { useEffect, useState, useCallback } from "react";
import DrawEntriesList from "./DrawEntriesList";
import { useAuth0 } from "@auth0/auth0-react";

function DrawEntries() {
  const [entries, setEntries] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const { getAccessTokenSilently } = useAuth0();

  const FetchEntriesHander = useCallback(async () => {
    setError(null);
    setIsLoading(true);

    try {
      const accessToken = await getAccessTokenSilently({
        audience: "https://localhost:44362/api",
        scope: "read:current_user",
      });

      const response = await fetch("/api/ticket/all", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${accessToken}`,
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
      {!isLoading && !error && entries.length > 0 && (
        <DrawEntriesList items={entries} />
      )}
      {!isLoading && !error && entries.length === 0 && <p>No Entries!</p>}
      {isLoading && <p>Loading...</p>}
      {!isLoading && error && <p>{error}</p>}
    </>
  );
}

export default DrawEntries;
