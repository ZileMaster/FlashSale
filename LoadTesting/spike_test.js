import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  // SIMULATION SETTINGS
  // We want 50 "Virtual Users" (VUs) hitting the API at once
  vus: 50,
  duration: '10s', 
};

export default function () {

  const eventId = 'e379cefb-e18e-4610-8f3c-2b5c7b4f1b43'; 
  
  const url = 'http://localhost:5231/api/Events/purchase'; 
  
  const payload = JSON.stringify({
    eventId: eventId,
    quantity: 1
  });

  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };
  const res = http.post(url, payload, params);

  check(res, {
    'is status 200': (r) => r.status === 200,
    'is status 400': (r) => r.status === 400, // Sold out
  });
  
  sleep(0.1);
}