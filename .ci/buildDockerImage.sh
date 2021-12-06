#!/bin/bash
set -e
set -u
set -x

docker build --file ./Challenges.API/Dockerfile --tag ghcr.io/nmshd/bkb-challenges:${TAG-temp} .
