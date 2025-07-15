import { Box, Button, Grid, Typography } from "@mui/material";
import EventCard from "./EventCard";
import { useEffect, useState } from "react";
import axios from "axios";

const categories = [
  "All",
  "Art",
  "Business",
  "Education",
  "Festival",
  "Food",
  "Nightlife",
  "Sports",
  "More",
];

export default function Home() {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios
      .get("http://localhost:5000/api/Event/GetAllEvents")
      .then((res) => {
        setEvents(res.data);
        setLoading(false);
      })
      .catch((err) => {
        console.error("Failed to fetch events", err);
        setLoading(false);
      });
  }, []);

  if (loading)
    return (
      <Typography align="center" mt={5}>
        Loading events...
      </Typography>
    );

  return (
    <Box p={4}>
      <Typography variant="h4" align="center" fontWeight={700}>
        Upcoming <span style={{ color: "#fbbc04" }}>Events</span>
      </Typography>
      <Typography align="center" color="text.secondary" mt={1} mb={3}>
        Join us for thrilling events! Live music, workshops, art exhibits, and
        more!
      </Typography>

      {/* Categories */}
      <Box
        gap="10px"
        display="flex"
        justifyContent="center"
        mb={4}
        flexWrap="wrap"
      >
        {categories.map((cat) => (
          <Button
            key={cat}
            variant="outlined"
            sx={{ borderRadius: 999, textTransform: "none", px: 2 }}
          >
            {cat}
          </Button>
        ))}
      </Box>

      <Grid container spacing={2}>
        {events.map((event, index) => (
          <Grid item xs={12} sm={6} md={4} key={index}>
            <Box height="100%">
              <EventCard event={event} />
            </Box>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}
