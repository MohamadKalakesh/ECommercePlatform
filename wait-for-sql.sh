#!/bin/sh
# wait-for-sql.sh

set -e
host="$1"
shift
cmd="$@"

until nc -z $host 1433; do
  echo "Waiting for SQL Server at $host:1433..."
  sleep 2
done

echo "SQL Server is up - executing command"
exec $cmd
