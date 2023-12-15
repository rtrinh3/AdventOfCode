#!/bin/sh

grep -oh '".*txt\"' *.cs | sort | uniq | xargs touch
