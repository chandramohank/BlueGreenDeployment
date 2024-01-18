# Base image: Nginx
FROM nginx:1.23-alpine

# Copy the HTML file to Nginx's document root
#COPY index.html /usr/share/nginx/html
RUN mkdir -p /usr/share/nginx/html/.well-known
COPY cert/apple-developer-merchantid-domain-association /usr/share/nginx/html/.well-known

# Expose port 80 for Nginx
EXPOSE 80

# Start Nginx in the foreground
CMD ["nginx", "-g", "daemon off;"]