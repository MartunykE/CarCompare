docker exec -it spareparts.api.mongo1 mongo

rs.initiate(
  {
    _id : 'rs0',
    members: [
      { _id : 0, host : "spareparts.api.mongo1:27017" },
      { _id : 1, host : "spareparts.api.mongo2:27017" },
      { _id : 2, host : "spareparts.api.mongo3:27017", arbiterOnly: true }
    ]
  }
)

exit