// src/components/EventCard.jsx
import { Box, Button, Typography } from "@mui/material";

export default function EventCard({ event }) {
  return (
    <Box
      borderRadius={2}
      overflow="hidden"
      boxShadow={2}
      bgcolor="#fff"
      height="100%" // ⬅️ Ensure full height in Grid
      display="flex"
      width="350px"
      flexDirection="column" // ⬅️ Helps stack elements nicely
    >
      <img
        src={event.imagePath}
        alt={event.eventName}
        width="100%"
        height="180px"
        style={{ objectFit: "cover" }}
      />
      <Box p={2}>
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Button
            size="small"
            sx={{
              bgcolor: "#fbbc04",
              color: "#fff",
              fontSize: "10px",
              px: 1.5,
            }}
          >
            {event.date}
          </Button>
          <Button
            size="small"
            sx={{
              bgcolor: "#fbbc04",
              color: "#fff",
              fontSize: "10px",
              px: 1.5,
            }}
          >
            {event.price}
          </Button>
        </Box>
        <Typography variant="subtitle1" fontWeight={600}>
          {event.eventName}
        </Typography>
        <Box
          display="flex"
          gap={1}
          alignItems="center"
          fontSize="12px"
          mt={1}
          color="text.secondary"
        >
          <span>{event.categoryID}</span> • <span>{event.date}</span> •{" "}
          <span>{event.place}</span>
        </Box>
        <Typography
          variant="body2"
          mt={1}
          color="text.secondary"
          sx={{
            overflow: "hidden",
            textOverflow: "ellipsis",
          }}
        >
          {event.eventName}
        </Typography>
      </Box>
    </Box>
  );
}
