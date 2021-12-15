
;;;======================================================
;;;   Automotive Expert System
;;;
;;;     This expert system diagnoses some simple
;;;     problems with a car.
;;;
;;;     CLIPS Version 6.3 Example
;;;
;;;     To execute, merely load, reset and run.
;;;======================================================

;;****************
;;* DEFFUNCTIONS *
;;****************

(deffunction ask-question (?question $?allowed-values)
   (printout t ?question)
   (bind ?answer (read))
   (if (lexemep ?answer) 
       then (bind ?answer (lowcase ?answer)))
   (while (not (member$ ?answer ?allowed-values)) do
      (printout t ?question)
      (bind ?answer (read))
      (if (lexemep ?answer) 
          then (bind ?answer (lowcase ?answer))))
   ?answer)

(deffunction yes-or-no-p (?question)
   (bind ?response (ask-question ?question yes no y n))
   (if (or (eq ?response yes) (eq ?response y))
       then yes 
       else no))

(deffunction yes-no-idk (?question)
   (bind ?response (ask-question ?question yes no idk y n i))
   (if (or (eq ?response yes) (eq ?response y)) then (return yes))
   (if (or (eq ?response no) (eq ?response n)) then (return no))
   (if (or (eq ?response idk) (eq ?response i)) then idk))
   

;;;***************
;;;* QUERY RULES *
;;;***************

(defrule R1 ""
   (declare (salience 9))
   (illegal-infinite-villagers yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-villagers "Infinite villagers source is available!")))

(defrule R2 ""
   (declare (salience 9))
   (infinite-baby-villagers yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-villagers "Infinite villagers source is available!")))

(defrule R3 ""
   (declare (salience 9))
   (infinite-cured-villagers yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-villagers "Infinite villagers source is available!")))

(defrule R4 ""
   (declare (salience 9))
   (illegal-infinite-baby-villagers yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-baby-villagers yes)))

(defrule R5 ""
   (declare (salience 9))
   (infinite-villagers-farm yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-baby-villagers yes)))

(defrule R6 ""
   (declare (salience 9))
   (infinite-zombie-villagers yes)
   (infinite-golden-apples yes)
   ( (infinite-weakness-splash yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-cured-villagers yes)))

(defrule R7 ""
   (declare (salience 9))
   (illegal-infinite-golden-apples yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-golden-apples yes)))

(defrule R8 ""
   (declare (salience 9))
   (infinite-apples yes)
   (infinite-gold-ingots yes)
   (crafting-table yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-golden-apples yes)))

(defrule R9 ""
   (declare (salience 9))
   (infinite-zombie-piglins yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-gold-ingots yes)))

(defrule R10 ""
   (declare (salience 9))
   (brewing-stand yes)
   (infinite-blaze-powder yes)
   (infinite-gunpowder yes)
   (infinite-weakness-potions yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-weakness-splash-potions yes)))

(defrule R11 ""
   (declare (salience 9))
   (infinite-blazes yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-blaze-powder yes)))

(defrule R12 ""
   (declare (salience 9))
   (blazes-spawner yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-blazes yes)))

(defrule R13 ""
   (declare (salience 9))
   (infinite-witches yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-blaze-powder yes)))

(defrule R14 ""
   (declare (salience 9))
   (active-witches-spawn-zone yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-witches yes)))

(defrule R15 ""
   (declare (salience 9))
   (infinite-witches yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-gunpowder yes)))

(defrule R16 ""
   (declare (salience 9))
   (infinite-creepers yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-gunpowder yes)))

(defrule R17 ""
   (declare (salience 9))
   (active-creepers-spawn-zone yes)
   (not (infinite-villagers ?))
   =>
   (assert (infinite-creepers yes)))

(defrule determine-illegal-infinite-villagers ""
   (declare (salience 8))
   (not (illegal-infinite-villagers ?))
   (not (infinite-villagers ?))
   =>
   (assert (illegal-infinite-villagers (yes-no-idk "Is illegal infinite villagers source available (yes/no/idk)? "))))

(defrule determine-illegal-infinite-baby-villagers ""
   (declare (salience 7))
   (not (illegal-infinite-baby-villagers ?))
   (not (infinite-villagers ?))
   =>
   (assert (illegal-infinite-baby-villagers (yes-no-idk "Is illegal infinite baby villagers source available (yes/no/idk)? "))))

(defrule determine-infinite-villagers-farm ""
   (declare (salience 7))
   (not (infinite-villagers-farm ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-villagers-farm (yes-no-idk "Is infinite villagers farm available (yes/no/idk)? "))))

(defrule determine-infinite-zombie-villagers ""
   (declare (salience 7))
   (not (infinite-zombie-villagers ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-zombie-villagers (yes-no-idk "Is infinite zombie-villagers source available (yes/no/idk)? "))))

(defrule determine-infinite-golden-apples ""
   (declare (salience 7))
   (not (infinite-golden-apples ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-golden-apples (yes-no-idk "Is infinite golden apples source available (yes/no/idk)? "))))

(defrule determine-illegal-infinite-golden-apples ""
   (declare (salience 6))
   (infinite-golden-apples idk)
   (not (illegal-infinite-golden-apples ?))
   (not (infinite-villagers ?))
   =>
   (assert (illegal-infinite-golden-apples (yes-no-idk "Is illegal infinite golden apples source available (yes/no/idk)? "))))

(defrule determine-infinite-apples ""
   (declare (salience 6))
   (infinite-golden-apples idk)
   (not (infinite-apples ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-apples (yes-no-idk "Is infinite apples source available (yes/no/idk)? "))))

(defrule determine-gold-ingots ""
   (declare (salience 6))
   (infinite-golden-apples idk)
   (not (infinite-gold-ingots ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-gold-ingots (yes-no-idk "Is infinite gold ingots source available (yes/no/idk)? "))))

(defrule determine-crafting-table ""
   (declare (salience 6))
   (infinite-golden-apples idk)
   (not (crafting-table ?))
   (not (infinite-villagers ?))
   =>
   (assert (crafting-table (yes-no-idk "Is crafting table available (yes/no/idk)? "))))

(defrule determine-infinite-zombie-piglins ""
   (declare (salience 5))
   (infinite-gold-ingots idk)
   (not (infinite-zombie-piglins ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-zombie-piglins (yes-no-idk "Is infinite zombie piglins source available (yes/no/idk)? "))))

(defrule determine-infinite-weakness-splash-potions ""
   (declare (salience 7))
   (not (infinite-weakness-splash-potions ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-weakness-splash-potions (yes-no-idk "Is infinite weakness splash potions source available (yes/no/idk)? "))))

(defrule determine-brewing-stand ""
   (declare (salience 6))
   (infinite-weakness-splash-potions idk)
   (not (brewing-stand ?))
   (not (infinite-villagers ?))
   =>
   (assert (brewing-stand (yes-no-idk "Is brewing stand available (yes/no/idk)? "))))

(defrule determine-infinite-blaze-powder ""
   (declare (salience 6))
   (infinite-weakness-splash-potions idk)
   (not (infinite-blaze-powder ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-blaze-powder (yes-no-idk "Is infinite blaze powder source available (yes/no/idk)? "))))

(defrule determine-infinite-gunpowder ""
   (declare (salience 6))
   (infinite-weakness-splash-potions idk)
   (not (infinite-gunpowder ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-gunpowder (yes-no-idk "Is infinite gunpowder source available (yes/no/idk)? "))))

(defrule determine-infinite-weakness-potions ""
   (declare (salience 6))
   (infinite-weakness-splash-potions idk)
   (not (infinite-weakness-potions ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-weakness-potions (yes-no-idk "Is infinite weakness potions source available (yes/no/idk)? "))))

(defrule determine-infinite-blazes ""
   (declare (salience 5))
   (infinite-blaze-powder idk)
   (not (infinite-blazes ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-blazes (yes-no-idk "Is infinite blazes source available (yes/no/idk)? "))))

(defrule determine-infinite-witches ""
   (declare (salience 5))
   (or (infinite-blaze-powder idk) (infinite-gunpowder idk))
   (not (infinite-witches ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-witches (yes-no-idk "Is infinite witches source available (yes/no/idk)? "))))

(defrule determine-infinite-creepers ""
   (declare (salience 5))
   (infinite-gunpowder idk)
   (not (infinite-creepers ?))
   (not (infinite-villagers ?))
   =>
   (assert (infinite-creepers (yes-no-idk "Is infinite creepers source available (yes/no/idk)? "))))

(defrule determine-blazes-spawner ""
   (declare (salience 4))
   (infinite-blazes idk)
   (not (blazes-spawner ?))
   (not (infinite-villagers ?))
   =>
   (assert (blazes-spawner (yes-no-idk "Is blazes spawner available (yes/no/idk)? "))))

(defrule determine-active-witches-spawn-zone ""
   (declare (salience 4))
   (infinite-witches idk)
   (not (active-witches-spawn-zone ?))
   (not (infinite-villagers ?))
   =>
   (assert (active-witches-spawn-zone (yes-no-idk "Is active witches spawn zone available (yes/no/idk)? "))))

(defrule determine-active-creepers-spawn-zone ""
   (declare (salience 4))
   (infinite-creeper idk)
   (not (active-creepers-spawn-zone ?))
   (not (infinite-villagers ?))
   =>
   (assert (active-creepers-spawn-zone (yes-no-idk "Is active creepers spawn zone available (yes/no/idk)? "))))

(defrule unavailable ""
   (not (illegal-infinite-villagers ?))
   (not (infinite-baby-villagers ?))
   (not (infinite-cured-villagers ?))
   =>
   (assert (infinite-villagers "Infinite villagers source is unavailable")))

(defrule unknown ""
  (declare (salience -10))
  (not (infinite-villagers ?))
  =>
  (assert (infinite-villagers "Could not determine if infinite villagers source is available")))

;;;********************************
;;;* STARTUP AND CONCLUSION RULES *
;;;********************************

(defrule system-banner ""
  (declare (salience 10))
  =>
  (printout t crlf crlf)
  (printout t "Infinite villagers source detector v 0.1.0")
  (printout t crlf crlf))

(defrule print-result ""
  (declare (salience 10))
  (infinite-villagers ?item)
  =>
  (printout t crlf crlf)
  (printout t "Conclusion:")
  (printout t crlf crlf)
  (format t " %s%n%n%n" ?item))

