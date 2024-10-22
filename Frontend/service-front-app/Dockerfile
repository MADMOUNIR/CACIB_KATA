# Use an official Node.js image for building the React app
FROM node:18-alpine AS build

# Set the working directory
WORKDIR /app

# Copy the package.json and install the dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the application files
COPY . ./

# Build the React application
RUN npm run build

# Use an nginx image to serve the built React app
FROM nginx:alpine

# Copy the built React app to the nginx HTML directory
COPY --from=build /app/build /usr/share/nginx/html

# Expose port 80 to serve the application
EXPOSE 80

# Start nginx when the container starts
CMD ["nginx", "-g", "daemon off;"]
