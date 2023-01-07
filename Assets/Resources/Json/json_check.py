import json
with open("./cards.json", "r") as st_json:
  st_python = json.load(st_json)

cards = st_python["cards"]

id = set()

for card in cards:
  if card["id"] in id:
    print("id error:"+str(card["id"]))
  if len(card["price"]) != 10:
    print("price len error:"+str(card["id"]))
  if len(card["effect"]) != 7:
    print("effect len error:"+str(card["id"]))
  if card["turn"] == 0:
    print("turn zero error:"+str(card["id"]))
  if card["slot"] > 4:
    print("slot too big error:"+str(card["id"]))
  if card["effect"][5] == 1:
    print("end card:"+str(card["id"]))
