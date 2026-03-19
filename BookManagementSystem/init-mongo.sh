mongosh -- "$MONGO_INITDB_DATABASE" <<EOF
    db.createCollection('healthchecks');
    db.healthchecks.insertOne({
        status: "UP"
    });
EOF